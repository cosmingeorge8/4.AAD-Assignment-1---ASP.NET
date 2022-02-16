using Microsoft.IdentityModel.Tokens;

namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 *
 * The class's purpose is to encapsulate the user authentication attempts
 */
public class UserLogin
{
    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsValid()
    {
       return !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
    }
}