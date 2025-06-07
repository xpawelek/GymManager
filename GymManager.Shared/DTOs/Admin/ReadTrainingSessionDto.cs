namespace GymManager.Shared.DTOs.Admin;

public class ReadTrainingSessionDto
{
    public int Id { get; set; }
    public int TrainerId { get; set; }
    
    public string TrainerFirstName { get; set; }
    
    public string TrainerSecondName { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public bool IsGroupSession { get; set; }
    public int? MemberId {get; set;}
}