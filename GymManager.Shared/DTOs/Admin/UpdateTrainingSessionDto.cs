using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class UpdateTrainingSessionDto
{
    public int? TrainerId { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime? StartTime { get; set; }
}