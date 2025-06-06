using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateSelfMembershipDto
    {
        [Required(ErrorMessage = "Membership type ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Membership type ID must be a positive number.")]
        public int MembershipTypeId { get; set; }
    }
}
