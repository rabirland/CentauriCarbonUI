using System.Text.Json.Nodes;

namespace CentauriCarbon.Dtos;

public class ResponseData<T>
    where T : IPrinterResponseData
{
    public long Cmd { get; set; }

    public T Data { get; set; }

    public string RequestID { get; set; } = string.Empty;

    public string MainboardID { get; set; } = string.Empty;

    public long TimeStamp { get; set; }
}
