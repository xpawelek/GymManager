using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Trainer;

public class CreateServiceRequestDto
{
    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }
    
    [Required]
    [StringLength(250)]
    public string ProblemNote {get; set;}
    
    public IFormFile? ImagePath { get; set; }
}