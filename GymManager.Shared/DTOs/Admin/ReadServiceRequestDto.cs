namespace GymManager.Shared.DTOs.Admin;

public class ReadServiceRequestDto
{
    public int Id { get; set; }
    public string ServiceProblemTitle { get; set; } = string.Empty;
    public string ProblemNote { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public bool IsResolved { get; set; }
}
