namespace GymManager.Models.DTOs.Member;

public class ReadWeeklyScheduleDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<ReadTrainingSessionDto> TrainingSessions { get; set; } = new();
}