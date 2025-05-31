namespace GymManager.Models.DTOs.Admin;

public class ReadServiceRequestDto
{
    public int Id { get; set; }
    public string ServiceProblemTitle { get; set; }
    public string ProblemNote {get; set;}
    public string? ImagePath { get; set; }
    
    public DateTime RequestDate { get; set; }
    public int? EquipmentId { get; set; }
}