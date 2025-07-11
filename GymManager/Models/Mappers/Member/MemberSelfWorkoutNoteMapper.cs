﻿using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfWorkoutNoteMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(WorkoutNote.Member))]
    [MapperIgnoreSource(nameof(WorkoutNote.MemberId))]
    [MapperIgnoreSource(nameof(WorkoutNote.Trainer))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainingSession))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainingSessionId))]
    public partial ReadSelfWorkoutNoteDto ToReadDto(WorkoutNote workoutNote);
    
    //GET ALL
    public partial List<ReadSelfWorkoutNoteDto> ToReadDtoList(List<WorkoutNote> workoutNotes);
}