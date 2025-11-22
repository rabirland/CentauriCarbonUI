namespace CentauriCarbon.Dtos;

public class CentauriCarbonAttributesResponse : IPrinterResponse
{
    public PrinterAttributes Attributes { get; set; } = new();

    public string MainboardId { get; set; } = string.Empty;

    public long Timestamp { get; set; }

    public string Topic { get; set; } = string.Empty;
}
