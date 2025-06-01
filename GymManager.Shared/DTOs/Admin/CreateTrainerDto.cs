using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class CreateTrainerDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    
    [Required]
    public string PhotoPath { get; set; }
}