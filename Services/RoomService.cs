using RoomReservations.Models;

namespace RoomReservations.Services;

/**
 * @author Mucalau Cosmin
 */

/**
 * RoomService, doesn't deliver food, but gives you some rooms tho
 *
 * Separates the logic from model classes to the presentation layer
 */
public static class RoomService
{
    /*
     * Store the list of rooms
     */
    private static List<Room> Rooms{get;}
    /*
     * Temp static counter used for ids
     */
    private static int _idCounter = 1;

    /**
     * static temp Constructor used for hardcoded test values
     */
    static RoomService() => Rooms = new List<Room>
    {
        new(2, "lecture room"){Id= _idCounter++},
        new(3,"seminar room"){Id= _idCounter++}
    };

    /**
     * Get all rooms
     */
    public static List<Room> GetAll() => Rooms;

    /**
     * Get room with id specified
     */
    public static Room? Get(int id) => Rooms.FirstOrDefault( r => r.Id == id);

    /**
     * Add a room
     * assign it an ID and add it to list
     */
    public static Room Add(Room room)
    {
        room.Id = _idCounter++;
        Rooms.Add(room);
        return room;
    }

    /**
     * Delete a room by ID
     */
    public static bool Delete(int id)
    {
        var room = Get(id);
        return room is not null && Rooms.Remove(room);
    }
    

    /**
     * Update a room
     * look for a room with matching id and update it
     */
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

    /**
     * Get a list of all the rooms with no reservation for the given DateTime
     */
    public static List<Room> GetFreeRooms(DateTime date)
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


    /**
     * Get a Collection of Dictionaries that list the status of a room per period
     */
    public static List<Dictionary<DateTime, Reservation?>> GetAllByPeriod(TimeSpan timeSpan)
    {
        var rooms = new List<Dictionary<DateTime, Reservation?>>();
        foreach (var room in Rooms)
        {
            rooms.Add(room.GetRoomStatus(timeSpan));
        }

        return rooms;
    }
}