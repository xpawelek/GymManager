using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Admin;

public class CreateTrainingSessionDto
{
    [Required]
    public int TrainerId { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    
    [Required]
    public TimeSpan Duration { get; set; }
    
    [Required]
    public bool IsGroupSession { get; set; }
    
    public int? MemberId {get; set;}
}