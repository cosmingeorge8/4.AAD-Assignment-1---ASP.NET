using RoomReservations.Models;
using RoomReservations.Models.Utils;

namespace RoomReservations.Interfaces;

/**
 * @author Mucalau Cosmin
 */
public interface IUserRepository
{
    Task<User> GetUser(UserLogin userLogin);
    
    Task<User> GetUser(string? userLogin);

    void Delete(string? username);

    void Update(User user);
}