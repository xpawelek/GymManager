using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Member;

public class ReadSelfWorkoutNote
{
    public int Id { get; set; }
    
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
    
    [StringLength(500)]
    public string? WorkoutInfo { get; set; }
    
    [Required]
    public DateTime WorkoutStartTime { get; set; }

    [Required]
    public int TrainerId { get; set; }
}