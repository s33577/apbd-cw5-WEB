using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase

{
    // get /api/reservation/
    [HttpGet]
    public IActionResult GetReservation([FromQuery] DateOnly? date, [FromQuery] int? roomId, [FromQuery] string? status)
    {
        var query = DataStore.Reservations.AsQueryable();
        if (date.HasValue)
        {
            query = query.Where (r => r.Date == date.Value);
        }

        if (roomId.HasValue)
        {
            query = query.Where(r => r.RoomId == roomId.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(r => r.Status == status);
        }
        return Ok(query.ToList());
        
    }

    [HttpGet("{id}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }
    
    //POST /api/reservations
    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        
        if (room == null)
        {
            return BadRequest();
        }

        if (!room.IsActive)
        {
            return BadRequest();
        }
        bool isOverlap = DataStore.Reservations.Any(r => r.RoomId == reservation.RoomId && r.Date == reservation.Date  && r.StartTime < reservation.EndTime && r.EndTime > reservation.StartTime);
        if (isOverlap)
        {
            return Conflict();
        }

        reservation.Id = DataStore.GetNextReservationId();
        DataStore.Reservations.Add(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);

    }
    
    
    
    
}