using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoomReservations.Models;
using RoomReservations.Models.Utils;
using RoomReservations.Services;

namespace RoomReservations.Controllers;


/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    /**
     * Helper field used for reading  appsettings.json
     */
    private readonly IConfiguration _configuration;

    /**
     * Constructor gets called by the framework, used to initialize the IConfiguration field.
     */
    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /**
     * Login
     */
    [HttpPost]
    public IActionResult Login(UserLogin userLogin)
    {
        /*
         * Validate the user, is the user is no valid, throw a BadRequest
         */
        if (!userLogin.IsValid()) return BadRequest(userLogin);

        /*
         * Try to get a user with matching credential
         */
        var user = UserService.Get(userLogin);

        /*
         * If none found, throw NotFound()
         */
        if (user is null) return NotFound();

        /*
         * If this statement is reached, generate a JWT and return it to the user
         */
        var token = GenerateToken(user);

        return Ok(token);

    }

    /**
     * Generate JWT token
     */
    private string GenerateToken(User user)
    {
        /*
         * Get the JWT key
         */
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:Key").Value));
        
        /*
         * Initialize claims
         */
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.Name, user.Name)
        };

        /*
         * Create token
         */
        var token = new JwtSecurityToken
        (
            _configuration.GetSection("Jwt:Issuer").Value,
            _configuration.GetSection("Jwt:Audience").Value,
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );

        /*
         * Write it
         */
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        /*
         * return it
         */
        return tokenString;
    }
}