namespace RoomReservations.Models;

public static class DateUtil
{
    public static DateOnly GetToday()
    {
        return DateOnly.FromDateTime(DateTime.Today);
    }

    public static DateOnly GetToDate(TimeSpan timeSpan)
    {
        return DateOnly.FromDateTime(DateTime.Now.Add(timeSpan));
    }

    public static bool IsBeforeToday(string date)
    {
        return date.CompareTo(GetToday()) < 0;
    }
}