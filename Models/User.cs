namespace RoomReservations.Models;

/**
 * @author Mucalau Cosmin
 */

/**
 * Data model for a User
 */
public class User
{
    /**
     * Username, unique identifier of an user
     */
    public string Username { get; set; }
    
    /**
     * EmailAddress
     * @not used in any logic
     */
    public string EmailAddress { get; set; }
    
    /**
     * Password
     * user for login
     */
    public string Password { get; set; }

    /**
     * Name
     * @not used in any logic
     */
    public string Name { get; set; }
}