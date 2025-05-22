namespace GymManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

public class TrainerAssignments
{
    //przypisanie trenera do klienta, jesli klient go wybral, umozliwienie kontatku z trenerem klientowi
    //id trainer member, cooperation start, cooperation finish (can be null)
    public int Id { get; set; }
    
    [Required]
    public int TrainerId { get; set; }
    
    [Required]
    public Trainer Trainer { get; set; }
    
    [Required]
    public int MemberId { get; set; }
    
    [Required]
    public Member Member { get; set; }
    
    public DateTime? StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; }
    
    public bool IsActive { get; set; }
}