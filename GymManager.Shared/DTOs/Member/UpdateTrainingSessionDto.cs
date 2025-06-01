using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class UpdateTrainingSessionDto
{
    [Required]
    public DateTime? StartTime { get; set; }
}