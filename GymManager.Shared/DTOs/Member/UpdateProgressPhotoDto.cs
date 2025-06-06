using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class UpdateProgressPhotoDto
    {
        [StringLength(500, ErrorMessage = "Comment must be at most 500 characters.")]
        public string? Comment { get; set; }

        public string? ImagePath { get; set; }

        public bool? IsPublic { get; set; }
    }
}
