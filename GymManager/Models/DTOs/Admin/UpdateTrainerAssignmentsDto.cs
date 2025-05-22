using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class UpdateTrainerAssignmentsDto
{
    public int? TrainerId { get; set; }
}