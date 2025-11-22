namespace CentauriCarbon.Dtos;

/// <summary>
/// The status of the lights.
/// </summary>
public class LightStatus
{
    /// <summary>
    /// Should have 3 number.
    /// </summary>
    public uint[] RgbLight { get; set; }

    public uint SecondLight { get; set; }
}
