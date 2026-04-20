using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    // GET api/rooms 
    [HttpGet]
    public IActionResult GetRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var query = DataStore.Rooms.AsQueryable();
        if (minCapacity.HasValue)
        {
            query = query.Where(r => r.Capacity > minCapacity.Value);
            
        }

        if (hasProjector.HasValue)
        {
            query = query.Where(r=> r.HasProjector == hasProjector.Value);
        }

        if (activeOnly.HasValue && activeOnly.Value)
        {
            query = query.Where(r => r.IsActive == true);
            
        }

        var response = query.Select(r => new RoomResponseDto()
        {
            Id = r.Id,
            Name = r.Name,
            BuildingCode = r.BuildingCode,
            Floor = r.Floor,
            Capacity = r.Capacity,
            HasProjector = r.HasProjector,
            IsActive = r.IsActive
        }).ToList();
        return Ok(response);
    }
    
    // get /api/rooms/id
    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        var responseDto = new RoomResponseDto()
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        };
        return Ok(responseDto);
    }
    
    // get api/rooms/buildings/buildingcode
    [HttpGet("buildings/{buildingCode}")]
    public IActionResult GetBuildings(string buildingCode)
    {
        var rooms = DataStore.Rooms.Where(r => r.BuildingCode.Equals(buildingCode));

        var resposne = rooms.Select(r => new RoomResponseDto
        {
            Id = r.Id,
            Name = r.Name,
            Floor = r.Floor,
            Capacity = r.Capacity,
            HasProjector = r.HasProjector,
            IsActive = r.IsActive

        }).ToList();
        return Ok(resposne);
    }
    
    //post api/rooms
    [HttpPost]
    public IActionResult CreateRoom([FromBody] CreateRoomDto roomDto)
    {
        if (roomDto == null)
        {
            return BadRequest();
        }

        var newRoom = new Room()
        {
            Id = DataStore.GetNextRoomId(),
            Name = roomDto.Name,
            BuildingCode = roomDto.BuildingCode,
            Floor = roomDto.Floor,
            Capacity = roomDto.Capacity,
            HasProjector = roomDto.HasProjector,
            IsActive = roomDto.IsActive
        };
        
        DataStore.Rooms.Add(newRoom);

        var response = new RoomResponseDto
        {
            Id = newRoom.Id,
            Name = newRoom.Name,
            BuildingCode = newRoom.BuildingCode,
            Floor = newRoom.Floor,
            Capacity = newRoom.Capacity,
            HasProjector = newRoom.HasProjector,
            IsActive = newRoom.IsActive

        };
        return CreatedAtAction(nameof(GetRoom), new { id = newRoom.Id }, response);

    }
    
    // put api/rooms/{id}
    
    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] UpdateRoomDto roomDto)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        room.Name = roomDto.Name;
        room.BuildingCode = roomDto.BuildingCode;
        room.Floor = roomDto.Floor;
        room.Capacity = roomDto.Capacity;
        room.HasProjector = roomDto.HasProjector;
        room.IsActive = roomDto.IsActive;

        var response = new RoomResponseDto
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        };
        return Ok(response);

    }
    
    
    // delete /api/rooms/id
    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        bool hasReservations = DataStore.Reservations.Any(res => res.RoomId == id);
        if (hasReservations)
        {
            return Conflict("Cannot delete room because it has active reservations.");
        }

        DataStore.Rooms.Remove(room);
        return NoContent();
    }
    
    

    
}