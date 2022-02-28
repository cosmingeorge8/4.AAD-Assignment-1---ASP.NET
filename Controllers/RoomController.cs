using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservations.Interfaces;
using RoomReservations.Models;
using RoomReservations.Repositories;
using RoomReservations.Services;

namespace RoomReservations.Controllers;

/**
 * @author Mucalau Cosmin
 */

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    /**
     * Get a list of all rooms 
     */
    [HttpGet]
    public ActionResult<List<Room>> GetAll() => Ok(_roomRepository.GetAllRoomsAsync());

    /**
     * Get Room with specified ID
     * returns 404 if no room was found
     */
    [HttpGet("id")]
    public ActionResult<Room> Get(int id)
    {
        var room = _roomRepository.GetRoomAsync(id);
        if (room.Result != null)
        {
            return Ok(room);
        }

        return NotFound(id);
    }

    /**
     * Returns a list of all the rooms free on given date
     */
    [HttpGet("date")]
    public ActionResult<List<Room>> GetFreeRooms(DateTime date)
    {
        return _roomRepository.GetFreeRooms(date);
    }
    
    /**
     * Returns a list of al the rooms for the given period and their status
     */
    [Authorize]
    [HttpGet("period")]
    public List<Dictionary<DateTime, Reservation?>> GetByPeriod(DateTime dateTime)
    {
        // var availableRooms = GetAllByPeriod(dateTime);
        
        // return availableRooms;
        return null;
    }
}