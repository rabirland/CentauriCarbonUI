namespace CentauriCarbon.Extensions;

public static class DateTimeExtensions
{
    public static long ToTimestamp(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }
}
