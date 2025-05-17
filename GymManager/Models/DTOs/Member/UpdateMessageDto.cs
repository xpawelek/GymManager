using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class UpdateMessageDto
{
    [StringLength(1000)]
    public string? MessageContent { get; set; }
}