using System.Text.Json.Serialization;

namespace CentauriCarbon.Dtos;

public class FileListResponseData : IPrinterResponseData
{
    public int Ack { get; set; }

    public FileListEntry[] FileList { get; set; }

    public class FileListEntry
    {
        public string Name { get; set; } = string.Empty;

        public int Type { get; set; }

        public long CreatedTime { get; set; }

        public long FileSize { get; set; }

        public double LayerHeight { get; set; }

        public long TotalLayers { get; set; }

        public double EstFilamentLength { get; set; }
    }
}
