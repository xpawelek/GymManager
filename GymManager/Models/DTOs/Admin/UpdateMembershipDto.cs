namespace GymManager.Models.DTOs.Admin;

public class UpdateMembershipDto
{
    public int? MembershipTypeId { get; set; } 
    public DateTime? StartDate { get; set; }  
    public DateTime? EndDate { get; set; }
    public bool? IsActive { get; set; }
}