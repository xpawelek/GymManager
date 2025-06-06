using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class CreateMembershipDto
    {
        [Required(ErrorMessage = "Member ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Member ID must be a positive number.")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Membership type ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Membership type ID must be a positive number.")]
        public int MembershipTypeId { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
