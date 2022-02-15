namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

public class Reservation
{
    public Reservation(Room room, User user, DateTime date)
    {
        Room = room;
        User = user;
        Date = date;
    }

    public int Id { get; set; }
    public Room Room { get; }

    public User User { get; }

    public DateTime Date { get; }
}