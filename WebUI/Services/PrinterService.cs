using CentauriCarbon;
using CentauriCarbon.Dtos;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WebUI.Services;

public class PrinterService
{
    private readonly SDCPClient _printerClient = new();

    private readonly Subject<PrinterStatus> _printerStatusSubject = new();

    private PrinterStatus? _lastPrinterStatus;

    /// <summary>
    /// The last message of type <see cref="CentauriCarbonStatusResponse"/> sent by the printer.
    /// </summary>
    public PrinterStatus? LastPrinterStatus => _lastPrinterStatus;

    public IObservable<PrinterStatus> StatusReceived => _printerStatusSubject.AsObservable();

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

    public Task ConnectAsync()
    {
        return _printerClient.Connect("ws://192.168.1.59:3030/websocket");
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
