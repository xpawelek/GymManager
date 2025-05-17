using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Trainer;

public class UpdateTrainingSessionDto
{
    [StringLength(500)]
    public string? Description { get; set; }
}