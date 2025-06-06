using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class UpdateSelfTrainerAssignmentsDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Trainer ID must be a positive number.")]
    public int? TrainerId { get; set; }
}