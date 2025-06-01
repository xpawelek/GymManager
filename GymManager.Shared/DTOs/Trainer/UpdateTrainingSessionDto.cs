using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer;

public class UpdateTrainingSessionDto
{
    [StringLength(500)]
    public string? Description { get; set; }
}