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
}
