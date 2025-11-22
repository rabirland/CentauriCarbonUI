using CentauriCarbon.Dtos;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Text.Json.Nodes;

namespace CentauriCarbon;

public class SDCPClient
{
    private readonly ClientWebSocket _websocket;
    private readonly Subject<IPrinterResponse> _printerMessages = new();

    public IObservable<IPrinterResponse> Messages => _printerMessages.AsObservable();

    public bool IsConnected => _websocket.State == WebSocketState.Open;

    public SDCPClient()
    {
        _websocket = new ClientWebSocket();
    }

    public async Task Connect(string url, CancellationToken token = default)
    {
        if (_websocket.State == WebSocketState.Open)
        {
            await _websocket.SendAsync(Array.Empty<byte>(), WebSocketMessageType.Close, true, token);
            await _websocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, token);
        }

        try
        {
            await _websocket.ConnectAsync(new Uri(url), token);

            if (_websocket.State == WebSocketState.Open)
            {
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// Sends a request to the printer.
    /// </summary>
    public async Task SendRequest(IPrinterRequest request)
    {
        var json = SDCPJsonSerializer.Serialize(request);
        var bytes = Encoding.UTF8.GetBytes(json);

        await _websocket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

    private void Receiver()
    {
        var responseBuilder = new StringBuilder();
        var receiveBuffer = new byte[4096];
        var tokenSource = new CancellationTokenSource();
        var stream = new MemoryStream(receiveBuffer);
        var streamReader = new StreamReader(stream);
        int level = 0;

        while (_websocket.State == WebSocketState.Open)
        {
            stream.SetLength(receiveBuffer.Length);
            var result = _websocket.ReceiveAsync(receiveBuffer, tokenSource.Token).Result;
            stream.Position = 0;
            stream.SetLength(result.Count);

            while (streamReader.EndOfStream == false)
            {
                var character = (char)streamReader.Read();

                if (character == '{')
                {
                    level++;
                }
                else if (character == '}')
                {
                    level = Math.Max(0, level - 1);
                }

                if (level == 0)
                {
                    if (responseBuilder.Length > 0)
                    {
                        // Add the closing '}'
                        responseBuilder.Append(character);
                        OnMessage(responseBuilder.ToString());
                        responseBuilder.Clear();
                    }
                }
                else
                {
                    responseBuilder.Append(character);
                }
            }
        }
    }

    private void OnMessage(string json)
    {
        var node = SDCPJsonSerializer.Deserialize<JsonNode>(json);

        if (node == null)
        {
            return;
        }

        if (node["Status"] != null)
        {
            var report = SDCPJsonSerializer.Deserialize<CentauriCarbonStatusResponse>(node);

            if (report != null)
            {
                _printerMessages.OnNext(report);
            }
        }
        else if (node["Attributes"] != null)
        {
            var response = SDCPJsonSerializer.Deserialize<CentauriCarbonAttributesResponse>(json);

            if (response != null)
            {
                _printerMessages.OnNext(response);
            }
        }
        else if (node["Id"] != null)
        {
            var response = ParseDataResponse(node);

            if (response != null)
            {
                _printerMessages.OnNext(response);
            }
        }
        else
        {
            Console.WriteLine("Unknown message");
            Console.WriteLine(json);
        }
    }

    private IPrinterResponse? ParseDataResponse(JsonNode node)
    {
        if (node["Id"] == null)
        {
            return null;
        }

        var cmdCode = node["Data"]?["Cmd"]?.GetValue<int>();
        if (cmdCode == null)
        {
            return null;
        }

        IPrinterResponse? ret = cmdCode switch
        {
            CommandCodes.GetPrinterStatus => SDCPJsonSerializer.Deserialize<CentauriCarbonDataResponse<AckResponseData>>(node),
            CommandCodes.GetPrinterAttributes => SDCPJsonSerializer.Deserialize<CentauriCarbonDataResponse<AckResponseData>>(node),
            CommandCodes.GetPrintHistoryList => SDCPJsonSerializer.Deserialize<CentauriCarbonDataResponse<HistoryListResponseData>>(node),
            CommandCodes.GetFileList => SDCPJsonSerializer.Deserialize<CentauriCarbonDataResponse<FileListResponseData>>(node),
            _ => null,
        };

        return ret;
    }
}
