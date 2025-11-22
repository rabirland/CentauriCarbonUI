namespace CentauriCarbon.Dtos;

/// <summary>
/// The status report of the fans.
/// </summary>
public class FanStatus
{
    public uint AuxiliaryFan { get; set; }

    public uint BoxFan { get; set; }

    public uint ModelFan { get; set; }
}
