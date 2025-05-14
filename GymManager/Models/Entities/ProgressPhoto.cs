using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class ProgressPhoto
{
    //id, member, photo, date
    public int Id { get; set; }
    
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public Member Member { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    public string? Comment { get; set; }
    
    [Required]
    public string ImagePath { get; set; }
    
    public bool IsPublic { get; set; } = false;
}