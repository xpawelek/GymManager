namespace GymManager.Models.DTOs.Admin;

public class UpdateWeeklyScheduleDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<CreateTrainingSessionDto>? TrainingSessions { get; set; } = new();
}