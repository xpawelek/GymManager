using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Admin
{
    public class UpdateMembershipDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Membership type ID must be a positive number.")]
        public int? MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }
    }
}