namespace GymManager.Models.DTOs.Member;

public class ReadSelfTrainerAssignmentDto
{
    public int Id { get; set; }
    public int TrainerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}