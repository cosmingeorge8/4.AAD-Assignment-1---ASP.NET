public static class RoomReservationService
{
    private static List<Room> Rooms{get;}

    static RoomReservationService() => Rooms = new List<Room>
        {
            new Room{Id= 1, Floor = 2, Description = "Presentation Room"},
            new Room{Id= 2, Floor = 3, Description = "Cool Room"}
        };

     public static List<Room> GetAll() => Rooms;

     public static Room? Get(int Id) => Rooms.FirstOrDefault( r => r.Id == Id);

     public static void Add(Room room)
     {
         throw new Exception("Add room not implemented");
     }   

}