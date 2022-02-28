using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Models.Utils;
using RoomReservations.Models.Utils.Database;

namespace RoomReservations.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly DataContext _dataContext;

    public ReservationRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _dataContext.Reservations.ToListAsync();
    }

    public async Task<Reservation?> GetReservationAsync(int id)
    {
        return await _dataContext.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
    }

    public EntityEntry<Reservation> Delete(int id)
    {
        var reservation = GetReservationAsync(id);
        if (reservation.Result is null)
        {
            throw new Exception("Reservation with id not found");
        }
        _dataContext.SaveChanges();
        return  _dataContext.Reservations.Remove(reservation.Result);
    }
    
    public Reservation CreateReservation(int roomId, User user, DateTime date)
    {
        var room = _dataContext.Rooms.FindAsync(roomId);
        
        if (DateUtil.IsBeforeToday(date))
        {
            throw new Exception("No time travel yet, so no past tense booking");
        }
        if (room.Result is null)
        {
            throw new Exception("Room not found");
        }

        if (!room.Result.IsFree(date))
        {
            throw new Exception("Rooms is booked in " + date);
        }

        var reservation = new Reservation
        {
            Room = room.Result,
            User = user,
            Date = date,
        };

        BookRoom(room.Result, reservation);
        _dataContext.SaveChanges();
        return reservation;
    }

    private  void BookRoom(Room room, Reservation reservation)
    {
        room.AddReservation(reservation);
    }

}