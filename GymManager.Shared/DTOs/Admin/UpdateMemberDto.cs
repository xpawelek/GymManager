using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin;

public class UpdateMemberDto
{
    [StringLength(50)]
    public string? FirstName { get; set; } 
    
    [StringLength(50)]
    public string? LastName { get; set; } 
    
    
    public DateTime? DateOfBirth { get; set; } 
    
    [StringLength(15)]
    [Phone]
    public string? PhoneNumber { get; set; } 
}