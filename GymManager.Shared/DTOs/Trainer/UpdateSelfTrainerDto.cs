using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Trainer
{
    public class UpdateSelfTrainerDto
    {
        [StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
        public string? PhoneNumber { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
