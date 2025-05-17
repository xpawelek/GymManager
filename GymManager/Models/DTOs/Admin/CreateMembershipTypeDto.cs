using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Models.DTOs.Admin;

public class CreateMembershipTypeDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int DurationInDays { get; set; }
    
    [Required]
    public bool IncludesPersonalTrainer { get; set; }
    
    public int? PersonalTrainingsPerWeek { get; set; }
    
    [Required]
    public bool AllowTrainerSelection { get; set; }
    
    [Required]
    public bool IncludesProgressTracking { get; set; }
    
    public bool IsVisible { get; set; } = false;
}