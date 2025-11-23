using CentauriCarbon.Dtos;
using Microsoft.AspNetCore.Components;
using WebUI.Services;

namespace WebUI.Pages;

public partial class Home : IDisposable
{
    private List<IDisposable> _subscriptions = new();
    private PrinterStatusResponseParameter? _currentPrinterStatus;
    private string? _videoUrl = null;

    [Inject]
    public required PrinterService PrinterService { get; set; }

    public void Dispose()
    {
        foreach (IDisposable subscription in _subscriptions)
        {
            subscription.Dispose();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await PrinterService.ConnectAsync("192.168.1.59");
        await PrinterService.SendReportStatusCommand();
        await PrinterService.SendEnableVideoCommand();
    }

    protected override void OnParametersSet()
    {
        var sub1 = PrinterService.StatusResponse.Subscribe(x =>
        {
            _currentPrinterStatus = x;
            StateHasChanged();
        });

        _subscriptions.Add(sub1);

        var sub2 = PrinterService.SetVideoResponse.Subscribe(x =>
        {
            _videoUrl = x.VideoUrl;
            StateHasChanged();
        });

        _subscriptions.Add(sub2);
    }
}
