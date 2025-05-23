namespace GymManager.Models.DTOs.Trainer;

public class ReadMembershipTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public bool IncludesPersonalTrainer { get; set; }
    public int? PersonalTrainingsPerMonth { get; set; }
    public bool AllowTrainerSelection { get; set; }
    public bool IncludesProgressTracking { get; set; }
}