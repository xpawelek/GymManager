using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerWorkoutNoteMapper
{
    //POST
    [MapperIgnoreTarget(nameof(WorkoutNote.Trainer))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainerId))]
    [MapperIgnoreTarget(nameof(WorkoutNote.Member))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainingSession))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainingSessionId))]
    [MapperIgnoreTarget(nameof(WorkoutNote.Id))]
    public partial WorkoutNote ToEntity(CreateSelfWorkoutNote dto);
    
    //GET ONE
    [MapperIgnoreSource(nameof(WorkoutNote.Trainer))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainerId))]
    [MapperIgnoreSource(nameof(WorkoutNote.Member))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainingSession))]
    [MapperIgnoreSource(nameof(WorkoutNote.TrainingSessionId))]
    public partial ReadSelfWorkoutNoteDto ToReadDto(WorkoutNote note);
    
    //GET ALL
    public partial List<ReadSelfWorkoutNoteDto> ToReadDtoList(List<WorkoutNote> workoutNotes);
    
    //PUT
    [MapperIgnoreTarget(nameof(WorkoutNote.Trainer))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainerId))]
    [MapperIgnoreTarget(nameof(WorkoutNote.Member))]
    [MapperIgnoreTarget(nameof(WorkoutNote.MemberId))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainingSession))]
    [MapperIgnoreTarget(nameof(WorkoutNote.TrainingSessionId))]
    [MapperIgnoreTarget(nameof(WorkoutNote.Id))]
    [MapperIgnoreTarget(nameof(WorkoutNote.WorkoutStartTime))]
    public partial void UpdateEntity(UpdateSelfWorkoutNoteDto dto, WorkoutNote workoutNote);
}