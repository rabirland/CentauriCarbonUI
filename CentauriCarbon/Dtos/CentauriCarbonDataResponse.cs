namespace CentauriCarbon.Dtos;
public class CentauriCarbonDataResponse<T> : IPrinterResponse
    where T : IPrinterResponseData
{
    public string Id { get; set; } = string.Empty;

    public ResponseData<T> Data { get; set; }

    public string Topic { get; set; } = string.Empty;
}
