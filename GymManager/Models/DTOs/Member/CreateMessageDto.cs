using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateMessageDto
{
    [Required]
    [StringLength(1000)]
    public string MessageContent { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    public int TrainerId { get; set; }
}