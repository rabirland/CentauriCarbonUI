using CentauriCarbon;
using CentauriCarbon.Dtos;
using System.Net.Sockets;

var token = new CancellationTokenSource();

//var client = new CentauriCarbonSDCPClient();

//Console.WriteLine("Connecting...");
//await client.Connect("ws://192.168.1.223:3030/websocket");

//client.Messages.Subscribe(r =>
//{
//    Console.WriteLine($"MSG: {r.GetType().Name}");

//    if (r is CentauriCarbonDataResponse<FileListResponseData> fileList)
//    {
//        if (fileList?.Data?.Data?.FileList != null)
//        {
//            foreach (var entry in fileList.Data.Data.FileList)
//            {
//                Console.WriteLine($"F: {entry.Name}");
//            }
//        }
//    }
//});

//await client.SendRequest(new CentauriCarbonDataRequest<FileListRequestData>()
//{
//    Data = new RequestData<FileListRequestData>()
//    {
//        RequestId = RequestId.NewRequestId(),
//        From = 1,
//        Cmd = CommandCodes.GetFileList,
//        Data = new FileListRequestData()
//        {
//            Url = "/local",
//        },
//    }
//});

//var client = new HttpClient();
//var connection = await client.GetAsync("ws://192.168.1.59:3030/websocket");