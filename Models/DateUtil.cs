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

    public static DateOnly GetStringAsDateOnly(string date)
    {
        return DateOnly.Parse(date);
    }

    public static bool IsBeforeToday(DateOnly date)
    {
        return date.CompareTo(GetToday()) < 0;
    }
}