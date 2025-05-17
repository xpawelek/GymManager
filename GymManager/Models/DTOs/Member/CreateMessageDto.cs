using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateMessageDto
{
    [Required]
    [StringLength(1000)]
    public string MessageContent { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int TrainerId { get; set; }
}