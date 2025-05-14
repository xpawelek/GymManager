using GymManager.Models.Entities;

namespace GymManager.Models.DTOs;

public class EquipmentDetailsDto : EquipmentDto
{
    public List<ServiceRequestDto> ServiceRequests { get; set; } = new();
}