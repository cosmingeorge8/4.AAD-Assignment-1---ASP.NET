namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

public class Room
{
    public int Id {get;set;}

    public int Floor {get;set;}

    public string? Description {get;set;}

    private Dictionary<DateOnly, Reservation?> _reservations = new();


    private Reservation? GetByDate(DateOnly date)
    {
        return _reservations.ContainsKey(date) ? _reservations[date] : null;
    }
    public bool IsFree(DateOnly date)
    {
        return GetByDate(date) == null;
    }

    public bool Book(User user, DateOnly date)
    {
        if (IsFree(date))
        {
            return false;
        }
        
        _reservations.Add(date,new Reservation(this, user, date));
        return true;
    }

    public Dictionary<DateOnly, Reservation?> GetRoomStatus(TimeSpan timeSpan)
    {
        var date = DateUtil.GetToday();
        var reservations = new Dictionary<DateOnly, Reservation?>();
        
        while (date < DateUtil.GetToDate(timeSpan))
        {
            reservations[date] = GetByDate(date);
            date.AddDays(1);
        }

        return reservations;
    }
}