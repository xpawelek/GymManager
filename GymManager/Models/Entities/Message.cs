namespace GymManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

public class Message
{
    //id, member, trainer, message
    public int Id { get; set; }
    
    [Required]
    [StringLength(1000)]
    public string MessageContent { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public Member Member { get; set; }
    
    [Required]
    public int TrainerId { get; set; }
    
    [Required]
    public Trainer Trainer { get; set; }
}