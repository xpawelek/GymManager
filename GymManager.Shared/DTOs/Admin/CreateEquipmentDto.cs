using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class CreateEquipmentDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Notes { get; set; }

    [Required]
    public string PhotoPath { get; set; }


    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}