
namespace RoomReservations.Models;

public class Reservation
{
    public Reservation(Room room, User user, DateOnly date)
    {
        Room = room;
        User = user;
        Date = date.ToString();
    }

    public int Id { get; set; }
    public Room Room { get; }

    public User User { get; }

    public String Date { get; }
}