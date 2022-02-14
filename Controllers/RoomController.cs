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

    [HttpGet]
    public ActionResult<List<Room>> GetAll() => RoomReservationService.GetAll();

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
}