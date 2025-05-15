using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class UpdateSelfMemberDto
{
    [StringLength(50)]
    public string? FirstName { get; set; } = String.Empty; 
    
    [StringLength(50)]
    public string? LastName { get; set; } = String.Empty; 
    
    [EmailAddress]
    public string? Email { get; set; } = String.Empty; 
    
    public DateTime? DateOfBirth { get; set; } 
    
    [StringLength(15)]
    [Phone]
    public string? PhoneNumber { get; set; } = String.Empty; 
}