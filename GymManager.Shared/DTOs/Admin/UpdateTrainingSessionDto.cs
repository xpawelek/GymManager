using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateTrainingSessionDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Trainer ID must be a positive number.")]
        public int? TrainerId { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }
    }
}
