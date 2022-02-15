namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

public class Room
{
    public int Floor { get; }

    public string Description { get; }

    public int Id {get;set;}
    
    private Dictionary<DateTime, Reservation?> _reservations = new();

    public Room(int floor, string description)
    {
        Floor = floor;
        Description = description;
    }

    private Reservation? GetByDate(DateTime date)
    {
        return _reservations.ContainsKey(date) ? _reservations[date] : null;
    }
    public bool IsFree(DateTime date)
    {
        return GetByDate(date) == null;
    }

    public bool Book(User user, DateTime date)
    {
        if (IsFree(date))
        {
            return false;
        }
        
        _reservations.Add(date,new Reservation(this, user, date));
        return true;
    }

    public Dictionary<DateTime, Reservation?> GetRoomStatus(TimeSpan timeSpan)
    {
        var date = DateTime.Now;
        var reservations = new Dictionary<DateTime, Reservation?>();
        
        while (date < DateUtil.GetToDate(timeSpan))
        {
            reservations[date] = GetByDate(date);
            date.AddDays(1);
        }

        return reservations;
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation.Date,reservation);
    }
}