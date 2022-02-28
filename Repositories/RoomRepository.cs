
using RoomReservations.Models;
using RoomReservations.Models.Utils;

namespace RoomReservations.Repositories;

public class RoomRepository
{
    private DataContext _dataContext;

    public async Task<Room?> Get(int roomId)
    {
        return await _dataContext.Rooms.FindAsync(roomId);
    }

}