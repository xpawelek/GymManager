using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer
{
    public class UpdateSelfWorkoutNoteDto
    {
        [StringLength(500, ErrorMessage = "Workout info must be at most 500 characters.")]
        public string? WorkoutInfo { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Current weight must be a non-negative number.")]
        public double? CurrentWeight { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Current height must be a non-negative number.")]
        public double? CurrentHeight { get; set; }
    }
}
