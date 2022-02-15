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

 [HttpPost]
 public IActionResult Create(Room reservation, String date)
 {
  var user = new User();
  Reservation created;

  try
  {
   created = ReservationService.Add(reservation, user, date);
  }
  catch (Exception e)
  {
   return BadRequest(e);
  }
  return CreatedAtAction(nameof(Create), new {id = created.Id}, reservation);
 }
 
 
}