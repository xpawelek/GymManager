using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace GymManager.Shared.DTOs.Admin;

public class CreateServiceRequestDto
{
    [Required]
    [StringLength(50)]
    public string ServiceProblemTitle { get; set; }
    
    [Required]
    [StringLength(250)]
    public string ProblemNote {get; set;}
    
    public DateTime RequestDate { get; set; }
    public string? ImagePath { get; set; }
    
    public int? EquipmentId { get; set; }
}