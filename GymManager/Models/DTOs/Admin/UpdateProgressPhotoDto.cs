using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class UpdateProgressPhotoDto
{ 
    public string? Comment { get; set; }
    
    public bool? IsPublic { get; set; }
}