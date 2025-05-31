using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class UpdateTrainingSessionDto
{
    [Required]
    public DateTime? StartTime { get; set; }
}