using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    // GET api/rooms 
    public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProject, [FromQuery] bool? activeOnly)
    {
        var query = DataStore.Rooms.AsQueryable();

        if (minCapacity.HasValue)
        {
            query = query.Where(r => r.Capacity >= minCapacity.Value);
        }

        if (hasProject.HasValue)
        {
            query = query.Where(r => r.HasProjector == hasProject.Value);
        }

        if (activeOnly.HasValue && activeOnly.Value)
        {
            query = query.Where(r => r.IsActive);
        }

        var response = query.Select(r => new RoomResponseDto
        {
            Id = r.Id,
            Name = r.Name,
            BuildingCode = r.BuildingCode,
            Capacity = r.Capacity,
            HasProjector = r.HasProjector,
            IsActive = r.IsActive,
        }).ToList();
        return Ok(response);
    }
    

    
}