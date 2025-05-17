using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class CreateTrainerAssignmentDto
{
    [Required]
    public int TrainerId { get; set; }

    [Required]
    public int MemberId { get; set; }
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    
}