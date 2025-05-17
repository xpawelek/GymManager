using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Trainer;

public class UpdateServiceRequestDto
{
    [StringLength(50)]
    public string? ServiceProblemTitle { get; set; }

    [StringLength(250)]
    public string? ProblemNote {get; set;}
    
    public IFormFile? Image { get; set; }
}