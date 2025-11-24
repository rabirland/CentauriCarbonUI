namespace CentauriCarbon.Dtos;

public class SetFanSpeedCommandParameter
{
    public TargetFanSpeeds TargetFanSpeed { get; }

    public SetFanSpeedCommandParameter(uint modelFan, uint auxiliaryFan, uint boxFan)
    {
        TargetFanSpeed = new(modelFan, auxiliaryFan, boxFan);
    }

    public record TargetFanSpeeds(uint ModelFan, uint AuxiliaryFan, uint BoxFan);
}
