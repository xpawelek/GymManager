using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class CreateServiceRequestDto
{
    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }
    
    [Required]
    [StringLength(250)]
    public string ProblemNote {get; set;}
    
    public DateTime RequestDate { get; set; }
    public string? Image { get; set; }
}