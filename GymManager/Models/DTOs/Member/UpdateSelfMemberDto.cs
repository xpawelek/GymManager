using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class UpdateSelfMemberDto
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