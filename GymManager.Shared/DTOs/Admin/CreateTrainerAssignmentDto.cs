using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class CreateTrainerAssignmentDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int MemberId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
}