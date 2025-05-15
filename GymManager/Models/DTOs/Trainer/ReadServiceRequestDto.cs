namespace GymManager.Models.DTOs.Trainer;

public class ReadServiceRequestDto
{
    public int Id { get; set; }
    public string ServiceProblemTitle { get; set; }
    public string ProblemNote {get; set;}
    public string? ImagePath { get; set; }
}