﻿using System.ComponentModel.DataAnnotations;
using GymManager.Models.Entities;

namespace GymManager.Models.DTOs.Trainer;

public class UpdateSelfWorkoutNoteDto
{
    [StringLength(500)]
    public string? WorkoutInfo { get; set; }
    public double? CurrentWeight { get; set; }
    public double? CurrentHeight { get; set; }
}