namespace Expenzio.Service.Extensions;

public static class DateTimeExtensions
{
    public static DateTime SpecifyKind(this DateTime dateTime, DateTimeKind kind)
    {
        dateTime = DateTime.SpecifyKind(dateTime, kind);
        return dateTime;
    }
}
