using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer;

public class UpdateSelfMessageDto
{
    [StringLength(1000)]
    public string? MessageContent { get; set; }
}