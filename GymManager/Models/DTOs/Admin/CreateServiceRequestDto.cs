using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class CreateServiceRequestDto
{
    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }
    
    [Required]
    [StringLength(250)]
    public string ProblemNote {get; set;}
    
    public IFormFile? ImagePath { get; set; }
    
    public int? EquipmentId { get; set; }
}