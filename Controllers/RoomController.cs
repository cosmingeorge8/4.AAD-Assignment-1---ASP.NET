using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;
using RoomReservations.Services;

namespace RoomReservations.Controllers;

/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route($"[controller]")]
public class RoomController : ControllerBase
{
    public RoomController()
    {

    }

    /**
     * Get a list of all rooms
     */
    [HttpGet]
    public ActionResult<List<Room>> GetAll() => RoomReservationService.GetAll();

    /**
     * Get Room with specified ID
     * returns 404 if no room was found
     */
    [HttpGet("id")]
    public ActionResult<Room> Get(int id)
    {
        var room = RoomReservationService.Get(id);
        if (room == null)
        {
            return NotFound();
        }

        return room;
    }

    /**
     * Delete a room with specified Id
     * Returns 404 if no rooms was found
     * Returns the list of remaining rooms
     */
    [HttpDelete("id")]
    public ActionResult<List<Room>> Delete(int id)
    {
        if (!RoomReservationService.Delete(id))
        {
            return NotFound();
        }
        return RoomReservationService.GetAll();
    }

    [HttpPost]
    public IActionResult Create(Room room)
    {
        if (room.Description is  null)
        {
            return BadRequest("One of the given fields is null");
        }
        Room created = RoomReservationService.Add(room);
        return Create(room);
    }
    
}