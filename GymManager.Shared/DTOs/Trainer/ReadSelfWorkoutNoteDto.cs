namespace GymManager.Shared.DTOs.Trainer;

public class ReadSelfWorkoutNoteDto
{
    public int Id { get; set; }
    
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }

    public string? WorkoutInfo { get; set; }

    public DateTime WorkoutStartTime { get; set; }

    public int MemberId { get; set; }
}