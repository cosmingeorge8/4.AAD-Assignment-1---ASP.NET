using Microsoft.IdentityModel.Tokens;

namespace RoomReservations.Models.Utils;

/**
 * @author Mucalau Cosmin
 *
 * The class's purpose is to encapsulate the user authentication attempts
 */
public class UserLogin
{
    public string Username { get; set; }

    public string Password { get; set; }

    /**
     * Check for empty strings
     * If either one is empty, method will return false
     */
    public bool IsValid()
    {
       return !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
    }
}