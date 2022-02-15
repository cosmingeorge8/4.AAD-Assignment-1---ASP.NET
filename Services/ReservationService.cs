
using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;

namespace RoomReservations.Services;

public class ReservationService
{
    private static List<Reservation> _reservations = new List<Reservation>();

    public static List<Reservation> GetAll() => _reservations;
    
    private static int _idCounter = 1;

    public static Reservation? Get(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public static Reservation Add(Room room, User user, String date)
    {
         var reservation = CreateReservation(room, user, date);
        _reservations.Add(reservation);
        return reservation;
    }

    private static Reservation CreateReservation(Room room, User user, string date)
    {
        var dateOnly = DateOnly.Parse(date);
        if (DateUtil.IsBeforeToday(date))
        {
            throw new Exception("Invalid date");
        }

        var reservation = new Reservation(room, user, dateOnly);
        return reservation;
    }

    public static bool Delete(int id)
    {
        var reservation = Get(id);
        return reservation is not null && _reservations.Remove(reservation);
    }
    

}