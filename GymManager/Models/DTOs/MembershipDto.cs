namespace GymManager.Models.DTOs;

public class MembershipDto
{
    public MemberDto Member { get; set; } = new();
    public MembershipTypeDto MembershipType { get; set; } = new();
    public bool IsActive { get; set; }
}