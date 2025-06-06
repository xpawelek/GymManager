using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class CreateTrainingSessionDto
    {
        [Required(ErrorMessage = "Trainer ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Trainer ID must be a positive number.")]
        public int TrainerId { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
        public int DurationInMinutes { get; set; }

        [Required(ErrorMessage = "Session type is required.")]
        public bool IsGroupSession { get; set; }

        public int? MemberId { get; set; }
    }
}
