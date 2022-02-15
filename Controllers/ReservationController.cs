using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;
using RoomReservations.Services;

namespace RoomReservations.Controllers;

/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route($"[controller]")]
public class ReservationController : ControllerBase
{
    public ReservationController()
    {
    }
   
    /**
     * Get a list of all reservations 
     */
    [HttpGet]
    public ActionResult<List<Reservation>> GetAll() => ReservationService.GetAll();
 
    /**
     * Delete a room with specified Id
     * Returns 404 if no rooms was found
     * Returns the list of remaining rooms
     */
    [HttpDelete("id")]
    public ActionResult<List<Reservation>> Delete(int id)
    {
        if (!ReservationService.Delete(id))
        {
            return NotFound();
        }
        return ReservationService.GetAll();
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
        var user = new User();
        Reservation created;

        try
        {
            created = ReservationService.Add(roomId, user, date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return CreatedAtAction(nameof(Create), new {id = created.Id}, created);
    }

}