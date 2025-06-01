using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminTrainingSessionMapper
{
    //POST
    [MapperIgnoreTarget(nameof(TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.Id))]
    public partial TrainingSession ToEntity(CreateTrainingSessionDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(TrainingSession.Trainer))]
    [MapperIgnoreSource(nameof(TrainingSession.Member))]
    [MapperIgnoreSource(nameof(TrainingSession.WorkoutNote))]
    public partial ReadTrainingSessionDto ToReadDto(TrainingSession trainingSession);
    //GET ALL
    public partial List<ReadTrainingSessionDto> ToReadDtoList(List<TrainingSession> trainingSessions);
    //PUT 
    [MapperIgnoreTarget(nameof(TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(TrainingSession.MemberId))]
    [MapperIgnoreTarget(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(TrainingSession.IsGroupSession))]
    public partial void UpdateEntity(UpdateTrainingSessionDto dto, TrainingSession trainingSession);
}