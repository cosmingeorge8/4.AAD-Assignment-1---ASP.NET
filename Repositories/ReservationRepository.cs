using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoomReservations.Interfaces;
using RoomReservations.Models;
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

        return  _dataContext.Reservations.Remove(reservation.Result);
    }
}