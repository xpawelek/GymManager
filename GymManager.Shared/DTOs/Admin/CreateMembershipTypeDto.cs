using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class CreateMembershipTypeDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be at most 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration in days is required.")]
        [Range(1, 3650, ErrorMessage = "Duration must be between 1 and 3650 days.")]
        public int DurationInDays { get; set; }

        [Required(ErrorMessage = "Trainer inclusion flag is required.")]
        public bool IncludesPersonalTrainer { get; set; }

        [Range(0, 100, ErrorMessage = "Personal trainings per month must be between 0 and 100.")]
        public int? PersonalTrainingsPerMonth { get; set; }

        [Required(ErrorMessage = "Trainer selection flag is required.")]
        public bool AllowTrainerSelection { get; set; }

        [Required(ErrorMessage = "Progress tracking flag is required.")]
        public bool IncludesProgressTracking { get; set; }

        public bool IsVisible { get; set; } = false;
    }
}
