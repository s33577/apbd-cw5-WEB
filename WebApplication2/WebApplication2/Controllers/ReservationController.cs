using Microsoft.AspNetCore.Mvc;
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
    
    
    
}