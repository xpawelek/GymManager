using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class UpdateServiceRequestDto
{
    [StringLength(50)]
    public string? ServiceProblemTitle { get; set; }

    [StringLength(250)]
    public string? ProblemNote {get; set;}
    
    public string? Image { get; set; }
    
    public int? EquipmentId { get; set; }
}