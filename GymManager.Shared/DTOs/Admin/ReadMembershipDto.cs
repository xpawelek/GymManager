namespace GymManager.Shared.DTOs.Admin;

public class ReadMembershipDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int MembershipTypeId { get; set; }  
    public DateTime StartDate { get; set; }  
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}
