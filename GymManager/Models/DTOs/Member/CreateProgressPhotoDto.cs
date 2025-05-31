using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateProgressPhotoDto
{
    [Required]
    public int MemberId { get; set; }
    
    public DateTime? Date { get; set; }
    
    [Required]
    public string? Comment { get; set; }
    
    [Required]
    public string ImagePath { get; set; }
    
    public bool IsPublic { get; set; } = false;
}