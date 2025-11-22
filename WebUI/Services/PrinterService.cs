using CentauriCarbon;
using CentauriCarbon.Dtos;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WebUI.Services;

public class PrinterService
{
    private string? _printerUrl;

    private readonly SDCPClient _printerClient = new();

    private readonly Subject<PrinterStatus> _printerStatusSubject = new();

    private PrinterStatus? _lastPrinterStatus;

    /// <summary>
    /// The last message of type <see cref="CentauriCarbonStatusResponse"/> sent by the printer.
    /// </summary>
    public PrinterStatus? LastPrinterStatus => _lastPrinterStatus;

    /// <summary>
    /// Fired whenever a <see cref="CentauriCarbonStatusResponse"/> is received from the printer.
    /// </summary>
    public IObservable<PrinterStatus> StatusReceived => _printerStatusSubject.AsObservable();

    /// <summary>
    /// The base URL for the printer or <see cref="string.Empty"/> when no printer is connected.
    /// </summary>
    public string PrinterUrl => _printerUrl ?? string.Empty;

    public PrinterService()
    {
        _printerClient.Messages
            .OfType<CentauriCarbonStatusResponse>()
            .Subscribe(response =>
            {
                if (response.Status != null)
                {
                    _lastPrinterStatus = response.Status;
                    _printerStatusSubject.OnNext(_lastPrinterStatus);
                }
            });
    }

    public Task ConnectAsync(string url)
    {
        _printerUrl = url;
        return _printerClient.Connect($"ws://{_printerUrl}:3030/websocket");
    }

    /// <summary>
    /// Sends a request to the printer to begin reporting the printer status.
    /// </summary>
    public async Task SendStatusRequest()
    {
        var requestData = new RequestData<EmptyRequestData>()
        {
            RequestId = RequestId.NewRequestId(),
            From = 1,
            Cmd = CommandCodes.GetPrinterStatus,
            Data = new EmptyRequestData(),
        };

        var request = new CentauriCarbonDataRequest<EmptyRequestData>()
        {
            Data = requestData,
        };

        await _printerClient.SendRequest(request);
    }
}
