using System.ComponentModel.DataAnnotations;


namespace GymManager.Shared.DTOs.Trainer;

public class CreateSelfWorkoutNote
{
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
    
    [StringLength(500)]
    public string? WorkoutInfo { get; set; }
    
    [Required]
    public DateTime WorkoutStartTime { get; set; }
    
    [Required]
    public int MemberId { get; set; }
}