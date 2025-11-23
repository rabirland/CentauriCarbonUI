using CentauriCarbon;
using CentauriCarbon.Dtos;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WebUI.Services;

public class PrinterService
{
    private string? _printerUrl;

    private readonly SDCPClient _printerClient = new();

    private readonly Subject<PrinterStatusResponseParameter> _printerStatusSubject = new();

    private PrinterStatusResponseParameter? _lastPrinterStatus;

    /// <summary>
    /// The last message of type <see cref="StatusResponseParameter"/> sent by the printer.
    /// </summary>
    public PrinterStatusResponseParameter? LastPrinterStatus => _lastPrinterStatus;

    /// <summary>
    /// Fired whenever a <see cref="StatusResponseParameter"/> is received from the printer.
    /// </summary>
    public IObservable<PrinterStatusResponseParameter> StatusResponse => _printerStatusSubject.AsObservable();

    /// <summary>
    /// Fired whenever the printer sends a <see cref="CommandCodes.SetVideoEnabled"/> response.
    /// </summary>
    public IObservable<SetVideoResponseParameter> SetVideoResponse => _printerClient.Messages.OfType<SetVideoResponseParameter>();

    /// <summary>
    /// The base URL for the printer or <see cref="string.Empty"/> when no printer is connected.
    /// </summary>
    public string PrinterUrl => _printerUrl ?? string.Empty;

    public PrinterService()
    {
        _printerClient.Messages
            .OfType<PrinterStatusResponseParameter>()
            .Subscribe(response =>
            {
                if (response != null)
                {
                    _lastPrinterStatus = response;
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
    /// Sends a command to the printer to begin reporting the printer status.
    /// </summary>
    public async Task SendReportStatusCommand()
    {
        var request = new CentauriCarbonCommand(
            Guid.NewGuid().ToString(),
            CommandCodes.GetPrinterStatus,
            new object());

        await _printerClient.SendCommand(request);
    }

    /// <summary>
    /// Sends a command to the printer to begin reporting the printer status.
    /// </summary>
    public async Task SendEnableVideoCommand()
    {
        var request = new CentauriCarbonCommand(
            Guid.NewGuid().ToString(),
            CommandCodes.SetVideoEnabled,
            new SetVideoEnabledCommandParameter(1));

        await _printerClient.SendCommand(request);
    }
}
