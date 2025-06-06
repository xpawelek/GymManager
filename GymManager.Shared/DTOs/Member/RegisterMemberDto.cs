namespace GymManager.Shared.DTOs.Member;

using System.ComponentModel.DataAnnotations;
using GymManager.Shared.Validation;

public class RegisterMemberDto
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name must be max 50 characters.")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name must be max 50 characters.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Date of birth is required.")]
    [DataType(DataType.Date)]
    [NotInFuture(ErrorMessage = "Date of birth must be in the past.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 9 digits.")]
    public string PhoneNumber { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = "";
}
