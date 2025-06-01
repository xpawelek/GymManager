using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class UpdateEquipmentDto
{
    [StringLength(50)]
    public string? Name { get; set; }
    
    [StringLength(300)]
    public string? Description { get; set; } 

    [StringLength(300)]
    public string? Notes { get; set; }

    [Range(0, int.MaxValue)]
    public int? Quantity { get; set; }
}