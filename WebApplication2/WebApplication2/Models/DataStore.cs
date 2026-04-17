namespace WebApplication2.Models;

public class DataStore
{
    public static List<Room> Rooms { get; set; } = new List<Room>
    {
        new Room
        {
            Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Storage",
            BuildingCode = "A",
            Floor = -1,
            Capacity = 5,
            HasProjector = false,
            IsActive = false

        },
        new Room
        {
            Id = 3,
            Name = "WorkShop",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 15,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
        Id = 4,
        Name = "Cafeteria",
        BuildingCode = "C",
        Floor = 1,
        Capacity = 45,
        HasProjector = false,
        IsActive = true
    }
    };

    public static List<Reservation> Reservations
    {
        get;
        set;
    } = new List<Reservation>
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OranizerName = "Bartek Szymonski",
            Topic = "APBD bascis",
            Date = new DateOnly(2021, 06, 15),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0),
            Status = "Completed",
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OranizerName = "Anna Kowalska",
            Topic = "REST API",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeSpan(12, 0, 0),
            EndTime = new TimeSpan(14, 0, 0),
            Status = "planned"
        }
    };
}