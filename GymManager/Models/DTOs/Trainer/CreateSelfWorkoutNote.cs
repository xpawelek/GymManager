using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Trainer;

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