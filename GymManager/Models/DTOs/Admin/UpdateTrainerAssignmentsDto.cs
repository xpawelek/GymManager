using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class UpdateTrainerAssignmentsDto
{
    public int? TrainerId { get; set; }

    public int? MemberId { get; set; }
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
}