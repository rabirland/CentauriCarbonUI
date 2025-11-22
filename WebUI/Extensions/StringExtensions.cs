namespace WebUI.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Returns <paramref name="placeholder"/> if the <paramref name="source"/> is null or empty. Otherwise return <paramref name="source"/>.
    /// </summary>
    public static string ReplaceIfEmpty(this string source, string placeholder)
    {
        if (string.IsNullOrEmpty(source))
        {
            return placeholder;
        }
        else
        {
            return source;
        }
    }
}
