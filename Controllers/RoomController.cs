using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservations.Models;
using RoomReservations.Services;

namespace RoomReservations.Controllers;

/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route("[controller]")]
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

    /**
     * Returns a list of all the rooms free on given date
     */
    [HttpGet("date")]
    public ActionResult<List<Room>> GetFreeRooms(DateTime date)
    {
        return RoomService.GetFreeRooms(date);
    }
    
    /**
     * Returns a list of al the rooms for the given period and their status
     */
    [Authorize]
    [HttpGet("period")]
    public List<Dictionary<DateTime, Reservation?>> GetByPeriod(DateTime dateTime)
    {
        var availableRooms = RoomService.GetAllByPeriod(dateTime);
        
        return availableRooms;
    }
}