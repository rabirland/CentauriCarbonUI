using System.Text.Json.Serialization;

namespace CentauriCarbon.Dtos;

/// <summary>
/// A wrapper for the command requests and responses.
/// <see cref="Data"/> contains the details about the actual command.
/// </summary>
public class CentauriCarbonDataRequest<T> : IPrinterRequest
    where T : IPrinterRequestData
{
    public string Id { get; set; } = string.Empty;

    public required RequestData<T> Data { get; set; }
}
