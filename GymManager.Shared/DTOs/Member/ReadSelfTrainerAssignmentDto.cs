namespace GymManager.Shared.DTOs.Member;

public class ReadSelfTrainerAssignmentDto
{
    public int Id { get; set; }
    public int TrainerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    
    public string TrainerFirstName { get; set; }
    
    public string TrainerSecondName { get; set; }
    
}