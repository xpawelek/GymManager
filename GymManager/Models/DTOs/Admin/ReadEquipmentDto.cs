using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Admin;

public class ReadEquipmentDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public List<ReadServiceRequestDto> ServiceRequests { get; set; } = new(); // czytamy tutaj zgloszenia serwisowe
}