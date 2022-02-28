namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

/**
 * Model class to to handle a reservation
 */
public class Reservation
{
 /**
     * Unique identifier
     */
    public int Id { get; set; }
    
    /**
     * Room that was reserved
     */
    public virtual Room Room { get; set; }

    /**
     * User that made the reservation
     */
    public virtual User User { get; set; }

    /**
     * Date the reservation was made for
     */
    public DateTime Date { get; set; }
}