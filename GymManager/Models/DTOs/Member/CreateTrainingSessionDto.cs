using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateTrainingSessionDto
{
    [Required]
    public DateTime StartTime { get; set; }
}