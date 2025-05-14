using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.Entities;

public class Membership
{
    //subskrypcja - jaki typ? - jednorazowe wejscie nie wymaga tworzenia konta
    //id, member, type of subscription
    public int Id { get; set; }  // -> dto get: nie chcemy
    
    [Required]
    public int MemberId { get; set; }  // -> dto get: raczej nie chcemy

    [Required] public Member Member { get; set; }  // -> dto get: chcemy
    
    [Required]
    public int MembershipTypeId { get; set; }  // -> dto get: nie chcemy
    
    [Required]
    public MembershipType MembershipType { get; set; }  // -> dto get: chcemy
    
    [Required]
    public DateTime StartDate { get; set; }  // -> dto get: nie chcemy
    
    [Required]
    public DateTime? EndDate { get; set; }  // -> dto get: nie chcemy
    
    public bool IsActive => DateTime.Now >= StartDate && DateTime.Now <= EndDate;  // -> dto get: chcemy
}