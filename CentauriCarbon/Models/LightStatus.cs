namespace CentauriCarbon.Models;

/// <summary>
/// The status of the lights.
/// </summary>
public class LightStatus
{
    /// <summary>
    /// Should have 3 number.
    /// </summary>
    public uint[] RgbLight { get; set; } = Array.Empty<uint>();

    public uint SecondLight { get; set; }
}
