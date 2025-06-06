using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class UpdateSelfMemberDto
    {
        [StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
        public string? PhoneNumber { get; set; }
    }
}
