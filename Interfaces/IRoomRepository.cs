using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;

namespace RoomReservations.Interfaces;

/**
 * @author Mucalau Cosmin
 */
public interface IRoomRepository
{
    Task<List<Room>> GetAllRoomsAsync();

    Task<Room?> GetRoomAsync(int id);

    ActionResult<List<Room>> GetFreeRooms(DateTime date);
    
    List<Room> GetRoomsByPeriod(DateTime startPeriod, DateTime endPeriod);
}