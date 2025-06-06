using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateMembershipTypeDto
    {
        [StringLength(100, ErrorMessage = "Name must be at most 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? DurationInDays { get; set; }

        public bool? IncludesPersonalTrainer { get; set; }

        public int? PersonalTrainingsPerMonth { get; set; }

        public bool? AllowTrainerSelection { get; set; }

        public bool? IncludesProgressTracking { get; set; }

        public bool? IsVisible { get; set; }
    }
}