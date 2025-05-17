namespace GymManager.Models.DTOs.Member;

public class UpdateProgressPhotoDto
{
    public string? Comment { get; set; }
    public IFormFile? ImagePath { get; set; }
    public bool? IsPublic { get; set; } 
}