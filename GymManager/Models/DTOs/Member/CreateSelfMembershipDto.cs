using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Member;

public class CreateSelfMembershipDto
{
    [Required]
    public int MembershipTypeId { get; set; }  
}