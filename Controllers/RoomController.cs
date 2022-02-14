using Microsoft.AspNetCore.Mvc;

namespace RoomReservations.Controllers;

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
    public ActionResult<Room> Get(int Id)
    {
        var Room = RoomReservationService.Get(Id);
        if(Room == null)
            return NotFound();

        return Room;
    }
}