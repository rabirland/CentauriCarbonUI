namespace CentauriCarbon.Dtos;

public class RequestData<T>
    where T : IPrinterRequestData
{
    public int Cmd { get; set; }

    public required T Data { get; set; }

    public string RequestId { get; set; } = string.Empty;

    public string MainboardId { get; set; } = string.Empty;

    public long Timestamp { get; set; }

    public int From { get; set; }
}
