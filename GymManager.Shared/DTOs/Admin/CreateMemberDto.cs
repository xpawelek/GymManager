using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class CreateMemberDto
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = String.Empty; 

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = String.Empty; 
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty; 
    
    [Required]
    public DateTime DateOfBirth { get; set; } 
    
    [Required]
    [StringLength(15)]
    [Phone]
    public string PhoneNumber { get; set; } = String.Empty; 
}