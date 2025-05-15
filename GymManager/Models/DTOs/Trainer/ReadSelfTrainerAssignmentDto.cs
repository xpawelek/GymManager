namespace GymManager.Models.DTOs.Trainer;

public class ReadSelfTrainerAssignmentDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}