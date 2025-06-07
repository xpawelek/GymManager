using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateServiceRequestDto
    {
        [Required(ErrorMessage = "Service problem title is required."), MinLength(1)]
        [StringLength(50, ErrorMessage = "Service problem title must be at most 50 characters.")]
        public string ServiceProblemTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Problem note is required."), MinLength(1)]
        [StringLength(250, ErrorMessage = "Problem note must be at most 250 characters.")]
        public string ProblemNote { get; set; } = string.Empty;
    }
}
