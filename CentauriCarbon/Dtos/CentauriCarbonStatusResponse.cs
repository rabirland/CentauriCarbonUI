namespace CentauriCarbon.Dtos;

/// <summary>
/// The top level object that the printer reports when we connect to the websocket.
/// </summary>
public class CentauriCarbonStatusResponse : IPrinterResponse
{
    /// <summary>
    /// TODO: Probably some unique Id for each mainboard, or a hardware version?
    /// </summary>
    public string MainboardId { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the printer.
    /// </summary>
    public PrinterStatus? Status { get; set; }

    /// <summary>
    /// TODO: Unknown, maybe the packet's time.
    /// </summary>
    public ulong Timestamp { get; set; }

    /// <summary>
    /// TODO: Is it using MQ? Or just a common naming?
    /// </summary>
    public string? Topic { get; set; }
}
