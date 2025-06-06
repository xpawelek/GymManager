using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class UpdateTrainingSessionDto
{
    [Required(ErrorMessage = "Start time is required.")]
    [DataType(DataType.Date)]
    public DateTime? StartTime { get; set; }
}