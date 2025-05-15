using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Admin;

public class CreateWeeklyScheduleDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<CreateTrainingSessionDto> TrainingSessions { get; set; } = new();
}