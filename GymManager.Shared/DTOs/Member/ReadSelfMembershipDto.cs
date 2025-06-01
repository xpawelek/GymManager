namespace GymManager.Shared.DTOs.Member;

public class ReadSelfMembershipDto
{
    public int Id { get; set; }
    public int MembershipTypeId { get; set; }  
    public DateTime StartDate { get; set; }  
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
}