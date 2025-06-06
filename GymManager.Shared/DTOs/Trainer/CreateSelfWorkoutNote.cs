using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer
{
    public class CreateSelfWorkoutNote
    {
        [Range(0.0, double.MaxValue, ErrorMessage = "Current weight must be a non-negative number.")]
        public double? CurrentWeight { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Current height must be a non-negative number.")]
        public double? CurrentHeight { get; set; }

        [StringLength(500, ErrorMessage = "Workout info must be at most 500 characters.")]
        public string? WorkoutInfo { get; set; }

        [Required(ErrorMessage = "Workout start time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime WorkoutStartTime { get; set; }

        [Required(ErrorMessage = "Member ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Member ID must be a positive number.")]
        public int MemberId { get; set; }
    }
}
