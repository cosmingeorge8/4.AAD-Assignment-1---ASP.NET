using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RoomReservations.Interfaces;
using RoomReservations.Models;

namespace RoomReservations.Controllers;

/**
 * @author Mucalau Cosmin
 */

[Authorize]
[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationRepository _reservationRepository;

    private readonly IUserRepository _userRepository;

    public ReservationController(IReservationRepository reservationRepository, IUserRepository userRepository)
    {
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
    }
   
    /**
     * Get a list of all reservations 
     */
    [HttpGet]
    public ActionResult<List<Reservation>> GetAll() => Ok(_reservationRepository.GetAllReservationsAsync());

    /**
     * Get a reservation by ID
     */
    [HttpGet("id")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _reservationRepository.GetReservationAsync(id);
        if (reservation.Result is null)
        {
            return NotFound(id);
        }

        return Ok(reservation);
    }

    /**
     * Delete a room with specified Id
     * Returns 404 if no rooms was found
     * Returns the list of remaining rooms
     */
    [HttpDelete("id")]
    public ActionResult<List<Reservation>> Delete(int id)
    {
        EntityEntry<Reservation> reservation;
        try
        {
           reservation = _reservationRepository.Delete(id);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        return Ok(reservation);
    }

    /**
     * Book a room on a given date
     * Takes roomID and date as parameter
     * Checks for values and tries to create a Reservation object
     *
     * returns created object
     */
    [HttpPost]
    public IActionResult Create(int roomId, DateTime date)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = _userRepository.GetUser(username);
        if (user is null)
        {
            return NotFound(user);
        }
        Reservation created;
        try
        {
            created = _reservationRepository.CreateReservation(roomId, user, date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return CreatedAtAction(nameof(Create), new {id = created.Id}, created);
    }

}