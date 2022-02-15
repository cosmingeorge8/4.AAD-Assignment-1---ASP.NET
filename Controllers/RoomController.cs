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
    public ActionResult<List<Room>> GetAll() => RoomService.GetAll();

    /**
     * Get Room with specified ID
     * returns 404 if no room was found
     */
    [HttpGet("id")]
    public ActionResult<Room> Get(int id)
    {
        var room = RoomService.Get(id);
        if (room == null)
        {
            return NotFound();
        }

        return room;
    }

    [HttpGet("date")]
    public ActionResult<List<Room>> GetFreeRooms(String date)
    {
        var dateOnly = DateOnly.Parse(date);
        return RoomService.GetFreeRooms(dateOnly);
    }
    /**
     * Returns a list of al the rooms for the given period and their status
     */
    [HttpGet("period")]
    public List<Dictionary<DateOnly, Reservation?>> GetByPeriod(TimeSpan timeSpan)
    {
        var availableRooms = RoomService.GetAllByPeriod(timeSpan);

        return availableRooms;
    }
}