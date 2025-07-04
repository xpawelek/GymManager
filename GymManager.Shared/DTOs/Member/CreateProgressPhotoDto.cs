﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Shared.DTOs.Member
{
    public class CreateProgressPhotoDto
    {
        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(500, ErrorMessage = "Comment must be at most 500 characters.")]
        public string Comment { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public bool IsPublic { get; set; } = false;
    }
}
