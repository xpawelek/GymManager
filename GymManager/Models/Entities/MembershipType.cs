using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Models.Entities;

public class MembershipType
{
    //id, type - private trainer substrciption - some kind of occasion,
    //monthly (entrance), yearly, plus (one additional trainng per week possible - random trainer?),
    //pro (2 personal training sessions + full progress control + possiblity of chosing trainer) etc.
    
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    [Precision(10, 2)]
    public decimal Price { get; set; }
    
    [Required]
    public int DurationInDays { get; set; }
    
    [Required]
    public bool IncludesPersonalTrainer { get; set; }
    
    public int? PersonalTrainingsPerMonth { get; set; }
    
    [Required]
    public bool AllowTrainerSelection { get; set; }
    
    [Required]
    public bool IncludesProgressTracking { get; set; }
    
    public bool IsVisible { get; set; } = false;
    
    public ICollection<Membership> Memberships { get; set; }
}