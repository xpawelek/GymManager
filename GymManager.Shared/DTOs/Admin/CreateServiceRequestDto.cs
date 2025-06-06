using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class CreateServiceRequestDto
    {
        [Required(ErrorMessage = "Service title is required.")]
        [StringLength(50, ErrorMessage = "Service title must be at most 50 characters.")]
        public string ServiceProblemTitle { get; set; }

        [Required(ErrorMessage = "Problem note is required.")]
        [StringLength(250, ErrorMessage = "Problem note must be at most 250 characters.")]
        public string ProblemNote { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string? ImagePath { get; set; }

        public int? EquipmentId { get; set; }
    }
}