using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

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
  * Unique identifier
  */
   public int Id { get; set; }

   /**
     * Username
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
    [JsonIgnore]
    public string Password { get; set; }

    /**
     * Name
     * @not used in any logic
     */
    public string Name { get; set; }
}