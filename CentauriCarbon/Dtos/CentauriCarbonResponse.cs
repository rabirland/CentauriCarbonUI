using System.Text.Json.Serialization;

namespace CentauriCarbon.Dtos;
public class CentauriCarbonResponse
{
    public string? Id { get; init; }

    public ResponseData? Data { get; init; }

    public string? Topic { get; init; }

    public ulong Timestamp { get; set; }

    public string? MainboardId { get; set; }

    /// <summary>
    /// The response parameters unique to the response. It can be either the "Status" node, "Attributes" node, or the Data.Data node.
    /// It's separately serialized because it's place changes.
    /// </summary>
    [JsonIgnore]
    public object ResponseParameter { get; set; } = new();

    public record ResponseData(
        long Cmd,
        string RequestId,
        string MainboardId,
        long Timestamp);
}
