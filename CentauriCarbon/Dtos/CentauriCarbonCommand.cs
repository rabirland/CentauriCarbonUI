using CentauriCarbon.Extensions;

namespace CentauriCarbon.Dtos;

/// <summary>
/// A wrapper for the command requests and responses.
/// <see cref="Data"/> contains the details about the actual command.
/// </summary>
public class CentauriCarbonCommand
{
    public string Id { get; }

    public CommandDataData Data { get; }

    public CentauriCarbonCommand(string id, int commandCode, object commandParameters, string? requestId = null)
    {
        Id = id;

        Data = new CommandDataData(
            Cmd: commandCode,
            CommandParameters: commandParameters,
            RequestId: requestId ?? Guid.NewGuid().ToString(),
            MainboardId: Guid.NewGuid().ToString(),
            Timestamp: DateTime.Now.ToTimestamp(),
            From: 1);
    }

    /// <summary>
    /// Wrapper for the internal parameters of a centauri carbon request.
    /// </summary>
    public record CommandDataData(int Cmd, object CommandParameters, string RequestId, string MainboardId, long Timestamp, int From);
}
