using CentauriCarbon.Models;

namespace CentauriCarbon.Dtos;

public class FileListResponseParameter
{
    public int Ack { get; set; }

    public CentauriCarbonFile[] FileList { get; set; }
}
