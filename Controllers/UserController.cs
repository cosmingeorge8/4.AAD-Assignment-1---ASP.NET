using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoomReservations.Models;
using RoomReservations.Services;

namespace RoomReservations.Controllers;


/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UserController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpPost]
    public IActionResult Login(UserLogin userLogin)
    {
        if (!userLogin.IsValid()) return BadRequest(userLogin);

        var user = UserService.Get(userLogin);

        if (user is null) return NotFound();

        var token = GenerateToken(user);

        return Ok(token);

    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:Key").Value));
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.Name, user.Name)
        };

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

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenString;
    }
}