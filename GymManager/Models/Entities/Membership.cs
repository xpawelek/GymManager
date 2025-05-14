using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Membership
{
    //subskrypcja - jaki typ? - jednorazowe wejscie nie wymaga tworzenia konta
    //id, member, type of subscription
    public int Id { get; set; }
    
    [Required]
    public int MemberId { get; set; } 
    
    [Required]
    public Member Member { get; set; }
    
    [Required]
    public int MembershipTypeId { get; set; }
    
    [Required]
    public MembershipType MembershipType { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime? EndDate { get; set; }
    
    public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;
}