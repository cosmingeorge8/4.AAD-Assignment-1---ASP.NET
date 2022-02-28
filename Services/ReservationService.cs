// using RoomReservations.Models;
// using RoomReservations.Models.Utils;
//
// namespace RoomReservations.Services;
//
// /**
//  * @author Mucalau Cosmin
//  */
//
// public static class ReservationService
// {
//
//
//     public static Reservation Add(int roomId, User user, DateTime date)
//     {
//         return CreateReservation(roomId, user, date);
//     }
//
//     private static Reservation CreateReservation(int roomId, User user, DateTime date)
//     {
//         var room = RoomService.Get(roomId);
//         
//         if (DateUtil.IsBeforeToday(date))
//         {
//             throw new Exception("No time travel yet, so no past tense booking");
//         }
//         if (room is null)
//         {
//             throw new Exception("Room not found");
//         }
//
//         if (!room.IsFree(date))
//         {
//             throw new Exception("Rooms is booked in " + date);
//         }
//
//         var reservation = new Reservation
//         {
//             Room = room,
//             User = user,
//             Date = date,
//             Id = IdCounter++
//         };
//
//         BookRoom(room, reservation);
//         return reservation;
//     }
//
//     private static void BookRoom(Room room, Reservation reservation)
//     {
//         room.AddReservation(reservation);
//     }
//
//     
// }