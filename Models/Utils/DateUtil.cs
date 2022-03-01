namespace RoomReservations.Models.Utils;

/**
 * @author Mucalau Cosmin
 */

public static class DateUtil
{
    private static DateTime GetToday()
    {
        return DateTime.Today;
    }

    public static DateTime GetToDate(TimeSpan timeSpan)
    {
        return DateTime.Now.Add(timeSpan);
    }

    public static DateOnly GetStringAsDateOnly(string date)
    {
        return DateOnly.Parse(date);
    }

    public static bool IsBeforeToday(DateTime date)
    {
        return date.CompareTo(GetToday()) < 0;
    }

    public static bool ValidDates(DateTime startPeriod, DateTime endPeriod)
    {
        return endPeriod < startPeriod;
    }
}