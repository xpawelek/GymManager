using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer;

public class UpdateSelfWorkoutNoteDto
{
    [StringLength(500)]
    public string? WorkoutInfo { get; set; }
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
}