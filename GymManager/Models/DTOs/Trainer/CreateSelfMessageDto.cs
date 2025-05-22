using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Trainer;

public class CreateSelfMessageDto
{
    [Required]
    [StringLength(1000)]
    public string MessageContent { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public int MemberId { get; set; }
}