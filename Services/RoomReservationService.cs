using RoomReservations.Models;

namespace RoomReservations.Services;

/**
 * @author Mucalau Cosmin
 */

public static class RoomReservationService
{
    private static List<Room> Rooms{get;}
    private static int idCounter = 1;

    static RoomReservationService() => Rooms = new List<Room>
    {
        new Room{Id= idCounter++, Floor = 2, Description = "Presentation Room"},
        new Room{Id= idCounter++, Floor = 3, Description = "Cool Room"}
    };

    public static List<Room> GetAll() => Rooms;

    public static Room? Get(int id) => Rooms.FirstOrDefault( r => r.Id == id);

    public static Room Add(Room room)
    {
        room.Id = idCounter++;
        Rooms.Add(room);
        return room;
    }

    public static bool Delete(int id)
    {
        var room = Get(id);
        if (room is null)
        {
            return false;
        }

        return Rooms.Remove(room);
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
    
    

}