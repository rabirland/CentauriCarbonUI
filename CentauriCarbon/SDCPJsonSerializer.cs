using System.Text.Json;
using System.Text.Json.Nodes;

namespace CentauriCarbon;

public class SDCPJsonSerializer
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
    };

    public static T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public static T? Deserialize<T>(JsonNode json)
    {
        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}
