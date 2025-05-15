using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin;

public class UpdateMembershipTypeDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }
    
    public decimal? Price { get; set; }

    public int? DurationInDays { get; set; }

    public bool? IncludesPersonalTrainer { get; set; }
    
    public int? PersonalTrainingsPerWeek { get; set; }

    public bool? AllowTrainerSelection { get; set; }

    public bool? IncludesProgressTracking { get; set; }
    
    public bool? IsVisible { get; set; } 
}