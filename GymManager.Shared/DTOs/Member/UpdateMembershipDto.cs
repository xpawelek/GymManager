using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class UpdateMembershipDto
    {
        public int? MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
