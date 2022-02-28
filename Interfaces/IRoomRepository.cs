using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;

namespace RoomReservations.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllRoomsAsync();

    Task<Room?> GetRoomAsync(int id);

    ActionResult<List<Room>> GetFreeRooms(DateTime date);
}