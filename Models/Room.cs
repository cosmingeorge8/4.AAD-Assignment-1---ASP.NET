namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

/**
 * Model class for a Room
 */
public class Room
{
    /**
     * Floor the room is placed at..
     * Informational only, not used in any logic
     */
    public int Floor { get; set; }
    
    /**
     * Description of the room..
     * Informational only, not used in any logic
     */
    public string Description { get; set; }

    /**
     * Unique identifier
     */
    public int Id {get;set;}
    
    /**
     * Map to store the reservations made for this room
     * @key DateTime the date the reservation was made
     * @value The actual Reservation object
     */
    private Dictionary<DateTime, Reservation?> _reservations = new();
    

    /**
     * Get a reservation by date
     * Will return null if no value was found at key [date] 
     */
    private Reservation? GetByDate(DateTime date)
    {
        return _reservations.ContainsKey(date) ? _reservations[date] : null;
    }
    
    /**
     * Checks whether the room is booked in a certain date
     * Returns true if the room is free
     *         false if the room is booked
     */
    public bool IsFree(DateTime date)
    {
        return GetByDate(date) == null;
    }

    /**
     * TODO work in progress
     * Return the status of a room for a time period
     */
    public Dictionary<DateTime, Reservation?> GetRoomStatus(DateTime dateTime)
    {
        var date = DateTime.Now;
        var reservations = new Dictionary<DateTime, Reservation?>();
        
        while (date < dateTime)
        {
            reservations[date] = GetByDate(date);
            date = date.AddDays(1);
        }

        return reservations;
    }

    /**
     * Add a reservation
     * The date of the reservation will be used as key in the map
     */
    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation.Date,reservation);
    }

    /**
     * Remove a reservation from the list
     */
    public void RemoveReservation(Reservation reservation)
    {
        _reservations.Remove(reservation.Date);
    }

    /**
     * Get a List of all reservations
     */
    public IEnumerable<Reservation?> GetAllReservations()
    {
        return _reservations.Values.ToList();
    }
}