using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class UpdateSelfTrainerAssignmentsDto
{
    public int? TrainerId { get; set; }
    
}