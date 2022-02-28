
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Models.Utils.Database;

namespace RoomReservations.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly DataContext _dataContext;

    public RoomRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        InsertIfNull();
    }
    
    public async Task<List<Room?>> GetAllRoomsAsync()
    {
        return await _dataContext.Rooms.ToListAsync();
    }

    public async Task<Room?> GetRoomAsync(int id)
    {
        return await _dataContext.Rooms.FirstOrDefaultAsync( room => room != null && room.Id == id);
    }

    
    /**
     * Get a list of all the rooms with no reservation for the given DateTime
     */
    public ActionResult<List<Room>> GetFreeRooms(DateTime date)
    {
        var rooms = new List<Room>();

        foreach (var room in _dataContext.Rooms)
        {
            if (room != null && room.IsFree(date))
            {
                rooms.Add(room);
            }
        }

        return rooms;
    }

    public bool InsertIfNull()
    {
        var room1 = new Room()
        {
            Id = 1,
            Floor = 2,
            Description = "some cool room"
        };

        var room2 = new Room()
        {
            Id = 2,
            Floor = 3,
            Description = "lecture room"
        };

        if (_dataContext.Rooms.FindAsync(1).Result is not null ||
            _dataContext.Rooms.FindAsync(2).Result is not null) return false;
        _dataContext.Rooms.Add(room1);
        _dataContext.Rooms.Add(room2);
        _dataContext.SaveChanges();
        return true;

    }
}