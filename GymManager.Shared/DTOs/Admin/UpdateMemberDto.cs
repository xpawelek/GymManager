using System;
using System.ComponentModel.DataAnnotations;
using GymManager.Shared.Validation;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateMemberDto
    {
        [StringLength(50, ErrorMessage = "First name must be at most 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name must be at most 50 characters.")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [NotInFuture(ErrorMessage = "Date of birth must be in the past.")]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
        public string? PhoneNumber { get; set; }
    }
}