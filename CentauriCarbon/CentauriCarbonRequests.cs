using CentauriCarbon.Dtos;
using CentauriCarbon.Extensions;

namespace CentauriCarbon;

///// <summary>
///// A collection of common requests and request builders for the Centauri Carbon websocket communication.
///// </summary>
//public static class CentauriCarbonRequests
//{
//    /// <summary>
//    /// TODO: Purpose unknown, we only get an ack as response.
//    /// Sent immediately after connecting to websocket.
//    /// </summary>
//    public static RequestData Unknown1()
//    {
//        return new RequestData()
//        {
//            Cmd = CommandCodes.GetPrinterStatus,
//            Data = new EmptyRequestData(),
//            RequestId = RequestId.NewRequestId(),
//            MainboardId = string.Empty,
//            Timestamp = DateTime.Now.ToTimestamp(),
//            From = 1,
//        };
//    }

//    /// <summary>
//    /// TODO: Purpose unknown, we only get an ack as response.
//    /// Sent immediately after connecting to websocket.
//    /// </summary>
//    public static RequestData Unknown2()
//    {
//        return new RequestData()
//        {
//            Cmd = CommandCodes.GetPrinterAttributes,
//            Data = new EmptyRequestData(),
//            RequestId = RequestId.NewRequestId(),
//            MainboardId = string.Empty,
//            Timestamp = DateTime.Now.ToTimestamp(),
//            From = 1,
//        };
//    }
//    /// <summary>
//    /// Receives a bunch of GUIDs marked as "HistoryData".
//    /// Probably requests the print history.
//    /// </summary>
//    public static RequestData GetPrintHistoryList()
//    {
//        return new RequestData()
//        {
//            Cmd = CommandCodes.GetPrintHistoryList,
//            Data = new EmptyRequestData(),
//            RequestId = RequestId.NewRequestId(),
//            MainboardId = string.Empty,
//            Timestamp = DateTime.Now.ToTimestamp(),
//            From = 1,
//        };
//    }

//    /// <summary>
//    /// Gets the file list from the given URL in the parameters.
//    /// </summary>
//    /// <param name="fromUrl">The web UI uses '/local'</param>
//    public static RequestData GetFileList(string fromUrl)
//    {
//        return new RequestData()
//        {
//            Cmd = CommandCodes.GetFileList,
//            Data = new FileListRequestData()
//            {
//                Url = fromUrl,
//            },
//            RequestId = RequestId.NewRequestId(),
//            MainboardId = string.Empty,
//            Timestamp = DateTime.Now.ToTimestamp(),
//            From = 1,
//        };
//    }

//    /// <summary>
//    /// Gets the file list from the given URL in the parameters.
//    /// </summary>
//    /// <param name="fromUrl">The web UI uses '/local'</param>
//    public static RequestData SetFanSpeed(int modelFan, int auxiliaryFan, int boxFan)
//    {
//        return new RequestData()
//        {
//            Cmd = CommandCodes.SetFanSpeed,
//            Data = new FanSpeedRequestData()
//            {
//                TargetFanSpeed = new()
//                {
//                    ModelFan = modelFan,
//                    AuxiliaryFan = auxiliaryFan,
//                    BoxFan = boxFan,
//                }
//            },
//            RequestId = RequestId.NewRequestId(),
//            MainboardId = string.Empty,
//            Timestamp = DateTime.Now.ToTimestamp(),
//            From = 1,
//        };
//    }
//}
