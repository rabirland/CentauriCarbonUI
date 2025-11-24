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

    /// <summary>
    /// Subject to report the complete response from the printer.
    /// </summary>
    private readonly Subject<CentauriCarbonResponse> _printerResponses = new();

    /// <summary>
    /// Observable to receive only the response parameters sent by the printer.
    /// </summary>
    public IObservable<object> Messages => _printerResponses.Select(x => x.ResponseParameter);

    /// <summary>
    /// Observable to receive each full response from the printer.
    /// </summary>
    public IObservable<CentauriCarbonResponse> Responses => _printerResponses.AsObservable(); 

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
                // Run in "background"
                ReceiverTask();
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
    public async Task SendCommand(CentauriCarbonCommand request)
    {
        var json = SDCPJsonSerializer.Serialize(request);
        var bytes = Encoding.UTF8.GetBytes(json);

        await _websocket.SendAsync(bytes, WebSocketMessageType.Text, true, default);
    }

    private async Task ReceiverTask()
    {
        var responseBuilder = new StringBuilder();
        var receiveBuffer = new byte[4096];
        var tokenSource = new CancellationTokenSource();
        var stream = new MemoryStream(receiveBuffer);
        var streamReader = new StreamReader(stream);
        int level = 0;

        while (_websocket.State == WebSocketState.Open)
        {
            try
            {
                stream.SetLength(receiveBuffer.Length);
                var result = await _websocket.ReceiveAsync(receiveBuffer, tokenSource.Token);
                stream.Position = 0;
                stream.SetLength(result.Count);

                var buffer = await streamReader.ReadToEndAsync();

                for (int i = 0; i < buffer.Length; i++)
                {
                    var character = buffer[i];

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
                            var json = responseBuilder.ToString();
                            responseBuilder.Clear(); // Clear before processing, in case the JSON parsing fails, we should still empty the response builder.

                            OnMessage(json);
                        }
                    }
                    else
                    {
                        responseBuilder.Append(character);
                    }
                }

                // Allow the task system to do any other job
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"""
                    ERROR During printer response processing
                    {ex}
                    """);
            }
        }
    }

    private void OnMessage(string json)
    {
        var topLevelNode = SDCPJsonSerializer.Deserialize<JsonNode>(json);

        if (topLevelNode == null)
        {
            throw new Exception($"""
                Could not parse the SDCP response: {json}
                """);
        }

        var response = SDCPJsonSerializer.Deserialize<CentauriCarbonResponse>(topLevelNode);

        if (response == null)
        {
            throw new Exception($"""
                Could not deserialize the SDCP response:
                {json}
                """);
        }

        if (topLevelNode["Status"] is JsonNode statusNode and not null)
        {
            var report = SDCPJsonSerializer.Deserialize<PrinterStatusResponseParameter>(statusNode);

            if (report != null)
            {
                response.ResponseParameter = report;
            }
        }
        else if (topLevelNode["Attributes"] is JsonNode attributeNode and not null)
        {
            var attributes = SDCPJsonSerializer.Deserialize<PrinterAttributesResponseParameter>(attributeNode);

            if (attributes != null)
            {
                response.ResponseParameter = attributes;
            }
        }
        else if (topLevelNode["Id"] != null && topLevelNode["Data"] is JsonNode dataNode and not null)
        {
            var responseParameters = ParseDataResponse(dataNode);

            if (responseParameters != null)
            {
                response.ResponseParameter = responseParameters;
            }
        }
        else
        {
            throw new Exception($"""
                Unknown response variant:
                {json}
                """);
        }

        _printerResponses.OnNext(response);
    }

    private object ParseDataResponse(JsonNode? dataNode)
    {
        if (dataNode == null)
        {
            throw new Exception("""
                Unknown response parameter type.
                Expected RESPONSE[Data] to be not null.
                """);
        }

        var cmdCode = dataNode["Cmd"]?.GetValue<int>();
        if (cmdCode == null)
        {
            throw new Exception("""
                Unknown response command code.
                Can not parse integer from RESPONSE[Data][Cmd].
                """);
        }

        var responseParameterNode = dataNode["Data"];

        if (responseParameterNode == null)
        {
            throw new Exception("""
                Empty response parameter.
                Expected RESPONSE[Data][Data] to be not null.
                """);
        }

        object? ret = cmdCode switch
        {
            CommandCodes.GetPrinterStatus => SDCPJsonSerializer.Deserialize<AcknowledgeResponseParameter>(responseParameterNode),
            CommandCodes.GetPrinterAttributes => SDCPJsonSerializer.Deserialize<AcknowledgeResponseParameter>(responseParameterNode),
            CommandCodes.GetPrintHistoryList => SDCPJsonSerializer.Deserialize<HistoryListResponseParameter>(responseParameterNode),
            CommandCodes.GetFileList => SDCPJsonSerializer.Deserialize<FileListResponseParameter>(responseParameterNode),
            CommandCodes.SetVideoEnabled => SDCPJsonSerializer.Deserialize<SetVideoResponseParameter>(responseParameterNode),
            CommandCodes.SetFanSpeed => SDCPJsonSerializer.Deserialize<AcknowledgeResponseParameter>(responseParameterNode),
            _ => null,
        };

        if (ret == null)
        {
            throw new Exception($"""
                Could not parse response parameters.
                {responseParameterNode}
                """);
        }

        return ret;
    }
}
