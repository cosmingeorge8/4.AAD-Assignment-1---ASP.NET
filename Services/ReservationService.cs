
using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;

namespace RoomReservations.Services;

public static class ReservationService
{
    private static readonly List<Reservation> _reservations = new();

    public static List<Reservation> GetAll() => _reservations;

    private static int IdCounter = 1;

    private static Reservation? Get(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public static Reservation Add(Room room, User user, string date)
    {
         var reservation = CreateReservation(room, user, date);
        _reservations.Add(reservation);
        return reservation;
    }

    private static Reservation CreateReservation(Room requestedRoom, User user, string date)
    {
        var dateOnly = DateOnly.Parse(date);
        var room = RoomService.Get(requestedRoom.Id);
        
        if (DateUtil.IsBeforeToday(dateOnly))
        {
            throw new Exception("Invalid date");
        }
        if (room is null)
        {
            throw new Exception("Room not found");
        }

        if (!room.IsFree(DateUtil.GetStringAsDateOnly(date)))
        {
            throw new Exception("Rooms is booked in " + date);
        }

        var reservation = new Reservation(room, user, dateOnly)
        {
            Id = IdCounter++
        };
        return reservation;
    }

    public static bool Delete(int id)
    {
        var reservation = Get(id);
        return reservation is not null && _reservations.Remove(reservation);
    }
    

}