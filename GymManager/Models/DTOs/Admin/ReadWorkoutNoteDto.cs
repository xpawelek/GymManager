namespace GymManager.Models.DTOs.Admin;

public class ReadWorkoutNoteDto
{
    public int Id { get; set; }
    
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
    public string? WorkoutInfo { get; set; }
    public DateTime WorkoutStartTime { get; set; }
    public int MemberId { get; set; }
    public int TrainerId { get; set; }
    public int TrainingSessionId { get; set; }  
}