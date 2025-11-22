namespace CentauriCarbon.Dtos;

public class PrinterAttributes
{
    public string Name { get; set; } = string.Empty;
    public string MachineName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string ProtocolVersion { get; set; } = string.Empty;
    public string FirmwareVersion { get; set; } = string.Empty;
    public string XYZsize { get; set; } = string.Empty;
    public string MainboardIp { get; set; } = string.Empty;
    public string MainboardMAC { get; set; } = string.Empty;
    public string MainboardId { get; set; } = string.Empty;
    public int SDCPStatus { get; set; }
    public int MaximumCloudSDCPSercicesAllowed { get; set; }
    public int NumberOfCloudSDCPServicesConnected { get; set; }
    public int NumberOfVideoStreamConnected { get; set; }
    public int MaximumVideoStreamAllowed { get; set; }
    public string NetworkStatus { get; set; } = string.Empty;
    public int UsbDiskStatus { get; set; }
    public string[] Capabilities { get; set; } = Array.Empty<string>();
    public string[] SupportFileType { get; set; } = Array.Empty<string>();
    public DeviceStatus DevicesStatus { get; set; } = new();
    public int CameraStatus { get; set; }
    public long RemainingMemory { get; set; }
    public int TLPNoCapPos { get; set; }
    public int TLPStartCapPos { get; set; }
    public int TLPInterLayers { get; set; }
}

public class DeviceStatus
{
    public int SgStatus { get; set; }
    public int ZMotorStatus { get; set; }
    public int XMotorStatus { get; set; }
    public int YMotorStatus { get; set; }
}
