using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateProgressPhotoDto
    {
        [Required(ErrorMessage = "Member ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Member ID must be a positive number.")]
        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(500, ErrorMessage = "Comment must be at most 500 characters.")]
        public string Comment { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image path is required.")]
        public string ImagePath { get; set; } = string.Empty;

        public bool IsPublic { get; set; } = false;
    }
}
