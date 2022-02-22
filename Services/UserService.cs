using RoomReservations.Models;
using RoomReservations.Models.Utils;
using RoomReservations.Repositories;

namespace RoomReservations.Services;


public class UserService
{
    public static User? Get(UserLogin userLogin)
    {
        return UserRepository.Users.FirstOrDefault(user => 
            user.Username.Equals(userLogin.Username, StringComparison.Ordinal) && 
            user.Password.Equals(userLogin.Password));
    }
}