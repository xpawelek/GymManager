using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminWorkoutNoteMapper
{
    //GET ONE
    [MapperIgnoreSource(nameof(WorkoutNote.Trainer))]
    [MapperIgnoreSource(nameof(WorkoutNote.Member))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainingSession))]
    public partial ReadWorkoutNoteDto ToReadDto(WorkoutNote note);
    
    //GET ALL
    public partial List<ReadWorkoutNoteDto> ToReadDtoList(List<WorkoutNote> workoutNotes);
}