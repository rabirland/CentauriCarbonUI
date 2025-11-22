namespace CentauriCarbon.Dtos;

public class HistoryListResponseData : IPrinterResponseData
{
    public int Ack { get; set; }

    public string[] HistoryData { get; set; } = Array.Empty<string>();
}
