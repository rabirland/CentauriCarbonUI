namespace CentauriCarbon.Dtos;

public class FanSpeedRequestData : IPrinterRequestData
{
    public TargetFanSpeeds TargetFanSpeed { get; set; } = new();

    public class TargetFanSpeeds
    {
        public int ModelFan { get; set; }

        public int AuxiliaryFan { get; set; }

        public int BoxFan { get; set; }
    }
}
