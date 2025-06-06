using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateTrainingSessionDto
    {
        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        public int TrainerId { get; set; }

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters.")]
        public string? Description { get; set; }

        //[Required(ErrorMessage = "Group session flag is required.")]
        public bool IsGroupSession { get; set; }

        public int? MemberId { get; set; }
    }
}
