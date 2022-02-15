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

    public static Reservation Add(Room room, User user, DateTime date)
    {
         var reservation = CreateReservation(room, user, date);
        _reservations.Add(reservation);
        return reservation;
    }

    private static Reservation CreateReservation(Room requestedRoom, User user, DateTime date)
    {
        var room = RoomService.Get(requestedRoom.Id);
        
        if (DateUtil.IsBeforeToday(date))
        {
            throw new Exception("Invalid date");
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