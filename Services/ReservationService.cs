using RoomReservations.Models;
using RoomReservations.Models.Utils;

namespace RoomReservations.Services;

/**
 * @author Mucalau Cosmin
 */

public static class ReservationService
{
    public static List<Reservation> GetAll()
    {
        List<Reservation> reservations = new List<Reservation>();
        foreach (var room in RoomService.GetAll())
        {
            reservations.AddRange(room.GetAllReservations()!);
        }

        return reservations;
    }

    private static int IdCounter = 1;

    public static Reservation? Get(int id) => GetAll().FirstOrDefault(r => r.Id == id);

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

        var reservation = new Reservation
        {
            Room = room,
            User = user,
            Date = date,
            Id = IdCounter++
        };

        BookRoom(room, reservation);
        return reservation;
    }

    private static void BookRoom(Room room, Reservation reservation)
    {
        room.AddReservation(reservation);
    }

    public static bool Delete(int id)
    {
        var reservation = Get(id);
        if (reservation is null)
        {
            return false;
        }
        reservation.Room.RemoveReservation(reservation);
        return true;
    }
    
}