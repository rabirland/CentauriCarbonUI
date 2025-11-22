namespace CentauriCarbon.Dtos;

/// <summary>
/// The status report of the Centauri Carbon.
/// </summary>
public class PrinterStatus
{
    /// <summary>
    /// The current X Y Z coordinate of the print head, formatted as 0.00,0.00,0.00
    /// </summary>
    public string CurrentCoordinate { get; set; } = string.Empty;

    public FanStatus? CurrentFanSpeed { get; set; }

    /// <summary>
    /// TODO: Unknown
    /// </summary>
    public int[]? CurrentStatus { get; set; }

    public LightStatus? LightStatus { get; set; }

    public PrintInfo? PrintInfo { get; set; }

    /// <summary>
    /// TODO: Unknown
    /// </summary>
    public int PlatformType { get; set; }

    //public PrintInfo PrintInfo { get; set; }

    public double TempOfHotbed { get; set; }

    public double TempOfNozzle { get; set; }

    public double TempOfBox { get; set; }

    public double TempTargetBox { get; set; }

    public double TempTargetHotbed { get; set; }

    public double TempTargetNozzle { get; set; }

    /// <summary>
    /// TODO: Unknown
    /// </summary>
    public int TimeLapseStatus { get; set; }

    public double ZOffset { get; set; }
}
