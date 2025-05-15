using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateProgressPhotoDto
{
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    public string? Comment { get; set; }
    
    [Required]
    public IFormFile? ImagePath { get; set; }
    
    public bool IsPublic { get; set; } = false;
}