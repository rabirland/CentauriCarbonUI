using System.Text.Json.Serialization;

namespace CentauriCarbon.Models;

public class PrintInfo
{
    public int CurrentLayer { get; set; }

    /// <summary>
    /// TODO: Unknown, maybe CPU ticks or time ticks.
    /// </summary>
    public double CurrentTicks { get; set; }

    public string Filename { get; set; } = string.Empty;

    public int PrintSpeedPct { get; set; }

    public double Progress { get; set; }

    /// <summary>
    /// TODO: Unknown
    /// </summary>
    public int Status { get; set; }

    public string TaskId { get; set; } = string.Empty;

    public uint TotalLayer { get; set; }

    /// <summary>
    /// TODO: Maybe GCode commands??
    /// </summary>
    public double TotalTicks { get; set; }

    [JsonIgnore]
    public TimeSpan ElapsedTime => TimeSpan.FromSeconds(CurrentTicks);

    [JsonIgnore]
    public TimeSpan TotalTime => TimeSpan.FromSeconds(TotalTicks);
}
