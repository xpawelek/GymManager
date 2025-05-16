namespace GymManager.Models.DTOs.Admin;

public class ReadTrainerAssignmentDto
{
    public int Id { get; set; }
    public int TrainerId { get; set; }
    public int MemberId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }
}