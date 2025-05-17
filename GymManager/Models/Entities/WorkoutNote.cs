using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class WorkoutNote
{
    //notka to training session - opis co sie udalo zrobić na danym treninegu - umozliwa sledzenie psotepow, 
    //zapisywanie np planu treningu w celu dalszej progresji
    //id, date, hour, description, trainer, member
    
    public int Id { get; set; }
    
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
    
    [StringLength(500)]
    public string? WorkoutInfo { get; set; }
    
    [Required]
    public DateTime WorkoutStartTime { get; set; }
    
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public Member Member { get; set; }

    [Required]
    public int TrainerId { get; set; }
    
    [Required]
    public Trainer Trainer { get; set; }
    
    public int TrainingSessionId { get; set; }  

    public TrainingSession TrainingSession { get; set; } = null!; 
}