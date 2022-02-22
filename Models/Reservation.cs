namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

/**
 * Model class to to handle a reservation
 */
public class Reservation
{
    public Reservation(Room room, User user, DateTime date)
    {
        Room = room;
        User = user;
        Date = date;
    }

    /**
     * Unique identifier
     */
    public int Id { get; set; }
    
    /**
     * Room that was reserved
     */
    public Room Room { get; }

    /**
     * User that made the reservation
     */
    public User User { get; }

    /**
     * Date the reservation was made for
     */
    public DateTime Date { get; }
}