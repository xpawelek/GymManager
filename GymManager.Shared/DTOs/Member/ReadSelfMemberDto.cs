namespace GymManager.Shared.DTOs.Member;

public class ReadSelfMemberDto
{
    public int Id { get; set; } 
    public string FirstName { get; set; } = String.Empty;  
    public string LastName { get; set; } = String.Empty; 
    public string Email { get; set; } = String.Empty;  
    public DateTime DateOfBirth { get; set; }
    public string MembershipCardNumber { get; set; } = String.Empty;  
    public string PhoneNumber { get; set; } = String.Empty;  
}