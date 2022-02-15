using RoomReservations.Models;

namespace RoomReservations.Services;

/**
 * @author Mucalau Cosmin
 */

public static class RoomService
{
    private static List<Room> Rooms{get;}
    private static int _idCounter = 1;

    static RoomService() => Rooms = new List<Room>
    {
        new Room{Id= _idCounter++, Floor = 2, Description = "Presentation Room"},
        new Room{Id= _idCounter++, Floor = 3, Description = "Cool Room"}
    };

    public static List<Room> GetAll() => Rooms;

    public static Room? Get(int id) => Rooms.FirstOrDefault( r => r.Id == id);

    public static Room Add(Room room)
    {
        room.Id = _idCounter++;
        Rooms.Add(room);
        return room;
    }

    public static bool Delete(int id)
    {
        var room = Get(id);
        return room is not null && Rooms.Remove(room);
    }
    

    public static bool Update(Room room)
    {
        var index = Rooms.FindIndex(r => r.Id == room.Id);
        if (index == -1)
        {
            return false;
        }

        Rooms[index] = room;
        return true;
    }

    public static List<Room> GetFreeRooms(DateOnly date)
    {
        List<Room> rooms = new List<Room>();

        foreach (var room in Rooms)
        {
            if (room.IsFree(date))
            {
                rooms.Add(room);
            }
        }

        return rooms;
    }


    public static List<Dictionary<DateOnly, Reservation?>> GetAllByPeriod(TimeSpan timeSpan)
    {
        var rooms = new List<Dictionary<DateOnly, Reservation?>>();
        foreach (var room in Rooms)
        {
            rooms.Add(room.GetRoomStatus(timeSpan));
        }

        return rooms;
    }
}