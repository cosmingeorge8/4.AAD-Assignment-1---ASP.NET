using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoomReservations.Models;

namespace RoomReservations.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetAllReservationsAsync();

    Task<Reservation?> GetReservationAsync(int id);

    Task<Reservation> Delete(int id);

    Reservation CreateReservation(int roomId, User user, DateTime date);
}