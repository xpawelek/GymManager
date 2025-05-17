namespace GymManager.Models.DTOs.Trainer;

public class ReadEquipmentDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}