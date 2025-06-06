using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer
{
    public class UpdateServiceRequestDto
    {
        [StringLength(50, ErrorMessage = "Service problem title must be at most 50 characters.")]
        public string? ServiceProblemTitle { get; set; }

        [StringLength(250, ErrorMessage = "Problem note must be at most 250 characters.")]
        public string? ProblemNote { get; set; }

        [StringLength(300, ErrorMessage = "Image path must be at most 300 characters.")]
        public string? Image { get; set; }
    }
}
