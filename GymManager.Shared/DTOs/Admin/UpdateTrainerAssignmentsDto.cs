using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class UpdateTrainerAssignmentsDto
{
    public int? TrainerId { get; set; }
}