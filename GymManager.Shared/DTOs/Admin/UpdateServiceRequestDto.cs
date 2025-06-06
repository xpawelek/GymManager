using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateServiceRequestDto
    {
        [StringLength(50, ErrorMessage = "Title must be at most 50 characters.")]
        public string? ServiceProblemTitle { get; set; }

        [StringLength(250, ErrorMessage = "Problem note must be at most 250 characters.")]
        public string? ProblemNote { get; set; }

        public string? ImagePath { get; set; }

        public int? EquipmentId { get; set; }
    }
}
