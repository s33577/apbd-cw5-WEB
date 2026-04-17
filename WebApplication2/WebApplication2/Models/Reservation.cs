using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Reservation : IValidatableObject 

{
    public int Id { get; set; }
    
    public int RoomId { get; set; }
    
    [Required(ErrorMessage = "Org Name is required")]
    public string OranizerName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Topic is required")]
    public string Topic { get; set; } = string.Empty;
    
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Status { get; set; } = "Planned";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("EndTime must be greater than StartTime", new[] { nameof(EndTime)});
        }
    }
}