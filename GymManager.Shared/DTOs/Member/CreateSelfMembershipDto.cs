using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member;

public class CreateSelfMembershipDto
{
    [Required]
    public int MembershipTypeId { get; set; }  
}