namespace CentauriCarbon;

public static class RequestId
{
    private static readonly string ValidCharacters = "abvdefghijklmnopqrstuvwxyz0123456789";


    public static string NewRequestId()
    {
        char[] ret = new char[32];

        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = ValidCharacters[Random.Shared.Next(0, ValidCharacters.Length)];
        }

        return new string(ret);
    }
}
