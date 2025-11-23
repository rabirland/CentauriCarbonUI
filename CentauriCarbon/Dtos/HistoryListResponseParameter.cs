namespace CentauriCarbon.Dtos;

public class HistoryListResponseParameter
{
    public int Ack { get; set; }

    public string[] HistoryData { get; set; } = Array.Empty<string>();
}
