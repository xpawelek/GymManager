using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateTrainerAssignmentDto
{
    [Required]
    public int TrainerId { get; set; }
}