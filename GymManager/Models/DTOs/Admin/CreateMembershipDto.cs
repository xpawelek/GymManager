using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Models.DTOs.Admin
{
    public class CreateMembershipDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
