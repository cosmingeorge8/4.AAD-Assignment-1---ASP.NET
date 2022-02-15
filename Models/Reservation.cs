using System.Text.Json.Serialization;

namespace RoomReservations.Models;

public class Reservation
{
    public Reservation(Room room, User user, DateOnly date)
    {
        Room = room;
        User = user;
        Date = date;
    }

    public int Id { get; set; }
    public Room Room { get; }

    public User User { get; }

    public DateOnly Date { get; }
}