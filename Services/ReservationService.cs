using RoomReservations.Models;

namespace RoomReservations.Services;

/**
 * @author Mucalau Cosmin
 */

public static class ReservationService
{
    private static readonly List<Reservation> _reservations = new();

    public static List<Reservation> GetAll() => _reservations;

    private static int IdCounter = 1;

    private static Reservation? Get(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public static Reservation Add(int roomId, User user, DateTime date)
    {
        return CreateReservation(roomId, user, date);
    }

    private static Reservation CreateReservation(int roomId, User user, DateTime date)
    {
        var room = RoomService.Get(roomId);
        
        if (DateUtil.IsBeforeToday(date))
        {
            throw new Exception("No time travel yet, so no past tense booking");
        }
        if (room is null)
        {
            throw new Exception("Room not found");
        }

        if (!room.IsFree(date))
        {
            throw new Exception("Rooms is booked in " + date);
        }

        var reservation = new Reservation(room, user, date)
        {
            Id = IdCounter++
        };

        BookRoom(room, reservation);
        return reservation;
    }

    private static void BookRoom(Room room, Reservation reservation)
    {
        room.AddReservation(reservation);
        _reservations.Add(reservation);
    }

    public static bool Delete(int id)
    {
        var reservation = Get(id);
        return reservation is not null && _reservations.Remove(reservation);
    }
    
}