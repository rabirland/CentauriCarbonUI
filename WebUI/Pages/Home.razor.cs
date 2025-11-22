using Microsoft.AspNetCore.Components;
using System.Net.WebSockets;
using WebUI.Services;

namespace WebUI.Pages;

public partial class Home
{
    [Inject]
    public required PrinterService PrinterService { get; set; }

    [Inject]
    public required ClientWebSocket ws { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ws.ConnectAsync(new Uri("ws://192.168.1.59:3030/websocket"), default);
    }
}
