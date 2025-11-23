using CentauriCarbon.Dtos;
using Microsoft.AspNetCore.Components;
using WebUI.Services;

namespace WebUI.Pages;

public partial class Home : IDisposable
{
    private PrinterStatusResponseParameter? _currentPrinterStatus;

    [Inject]
    public required PrinterService PrinterService { get; set; }

    public void Dispose()
    {

    }

    protected override async Task OnInitializedAsync()
    {
        await PrinterService.ConnectAsync("192.168.1.59");
        await PrinterService.SendReportStatusCommand();
    }

    protected override void OnParametersSet()
    {
        PrinterService.StatusReceived.Subscribe(x =>
        {
            _currentPrinterStatus = x;
            StateHasChanged();
        });
    }
}
