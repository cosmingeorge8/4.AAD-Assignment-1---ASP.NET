using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Models.Utils;

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

    private readonly IUserRepository _userRepository;

    /**
     * Constructor gets called by the framework, used to initialize the IConfiguration field.
     */
    public UserController(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
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
        var user = _userRepository.GetUser(userLogin);

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

    [Authorize]
    [HttpDelete]
    public IActionResult Delete()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            _userRepository.Delete(username);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }

        return Ok();
    }

    [Authorize]
    [HttpPatch]
    public IActionResult Update(User user)
    {
        _userRepository.Update(user);
        return Ok();
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