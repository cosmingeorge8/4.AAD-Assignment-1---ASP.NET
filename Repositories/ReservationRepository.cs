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

    /**
     * Get a list of all reservations
     */
    public async Task<List<Reservation>> GetAllReservationsAsync()
    {
        return await _dataContext.Reservations.ToListAsync();
    }

    /**
     * Get a reservation by ID
     */
    public async Task<Reservation?> GetReservationAsync(int id)
    {
        return await _dataContext.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
    }

    /**
     * Delete a reservation
     */
    public async Task<Reservation> Delete(int id)
    {
        var reservation = await GetReservationAsync(id);
        if (reservation is null)
        {
            throw new Exception("Reservation with id not found");
        }

        _dataContext.Reservations.Remove(reservation);
        return reservation;
    }

    /**
     * Create a reservation
     */
    public Reservation CreateReservation(int roomId, User user, DateTime date)
    {
        var room = _dataContext.Rooms.FindAsync(roomId).Result;

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
        };

        _dataContext.Reservations.Add(reservation);
        room.AddReservation(reservation);

        _dataContext.Rooms.Update(room);
        _dataContext.SaveChanges();
        return reservation;
    }
}