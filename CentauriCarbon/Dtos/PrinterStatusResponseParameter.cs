using CentauriCarbon.Models;
using System.Text.Json.Serialization;

namespace CentauriCarbon.Dtos;

/// <summary>
/// The status report of the Centauri Carbon.
/// </summary>
public class PrinterStatusResponseParameter
{
    /// <summary>
    /// The current X Y Z coordinate of the print head, formatted as 0.00,0.00,0.00
    /// </summary>
    [JsonPropertyName("CurrenCoord")]
    public string? CurrentCoordinate
    {
        get => field;
        set
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                var coordinates = value.Split(',');

                if (coordinates.Length == 3)
                {
                    XCoordinate = double.Parse(coordinates[0]);
                    YCoordinate = double.Parse(coordinates[1]);
                    ZCoordinate = double.Parse(coordinates[2]);
                }
            }

            field = value;
        }
    }

    public FanStatus? CurrentFanSpeed { get; set; }

    /// <summary>
    /// TODO: Unknown
    /// </summary>
    public PrinterState[]? CurrentStatus { get; set; }

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

    [JsonIgnore]
    public double XCoordinate { get; private set; }

    [JsonIgnore]
    public double YCoordinate { get; private set; }

    [JsonIgnore]
    public double ZCoordinate { get; private set; }

    /// <summary>
    /// Gets the first entry in the <see cref="CurrentStatus"/> array, if not null and has any item. Otherwise returns <see cref="PrinterState.Unknown"/>.
    /// </summary>
    /// <returns></returns>
    public PrinterState GetPrinterState()
    {
        if (CurrentStatus?.Length > 0)
        {
            return CurrentStatus[0];
        }

        return PrinterState.Unknown;
    }
}
