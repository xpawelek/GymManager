using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class CreateTrainingSessionDto
{
    [Required]
    public int TrainerId { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public int DurationInMinutes { get; set; }
    
    [Required]
    public bool IsGroupSession { get; set; }
    
    public int? MemberId {get; set;}
}