using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer;

public class CreateSelfMessageDto
{
    [Required(ErrorMessage = "Message content is required.")]
    [StringLength(1000, ErrorMessage = "Message content must be at most 1000 characters.")]
    public string MessageContent { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    public int MemberId { get; set; }
}