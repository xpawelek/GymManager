namespace GymManager.Models.DTOs;

public class EquipmentDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int Quantity { get; set; }
}