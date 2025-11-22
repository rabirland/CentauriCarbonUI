using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace CentauriCarbon;

public class CentauriCarbonCommandClient
{
    private readonly Thread _receiveThread;
    private readonly UdpClient _client = new();
    private readonly Subject<string> _messageSubject = new();

    public IObservable<string> Messages => _messageSubject.AsObservable();

    public CentauriCarbonCommandClient()
    {
        _receiveThread = new Thread(new ThreadStart(Receiver));
    }

    public void Connect(string url)
    {
        _client.Connect(url, 3000);
        _receiveThread.Start();
    }

    public void Send(string command)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(command);
        _client.Send(bytes);
    }

    private void Receiver()
    {
        var remoteIp = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            var bytes = _client.Receive(ref remoteIp);
            var text = Encoding.UTF8.GetString(bytes);

            if (string.IsNullOrEmpty(text))
            {
                continue;
            }

            _messageSubject.OnNext(text);
        }
    }
}
