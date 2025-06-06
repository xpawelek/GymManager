using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer
{
    public class UpdateTrainingSessionDto
    {
        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }
    }
}
