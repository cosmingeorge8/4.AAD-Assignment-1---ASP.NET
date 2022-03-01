using RoomReservations.Models;
using RoomReservations.Models.Utils;

namespace RoomReservations.Interfaces;

public interface IUserRepository
{
    User? GetUser(UserLogin userLogin);
    
    User? GetUser(string? userLogin);


    void Delete(string? username);

    void Update(User user);
}