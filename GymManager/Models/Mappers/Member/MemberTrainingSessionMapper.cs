using GymManager.Models.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberTrainingSessionMapper
{
    //POST
    [MapperIgnoreTarget(nameof(TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(TrainingSession.TrainerId))]
    [MapperIgnoreTarget(nameof(TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(TrainingSession.MemberId))]
    [MapperIgnoreTarget(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.Description))]
    [MapperIgnoreTarget(nameof(TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(TrainingSession.IsGroupSession))]
    public partial TrainingSession ToEntity(CreateTrainingSessionDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(TrainingSession.Trainer))]
    [MapperIgnoreSource(nameof(TrainingSession.Member))]
    [MapperIgnoreSource(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.DurationInMinutes))]
    [MapperIgnoreSource(nameof(TrainingSession.DurationInMinutes))]
    public partial ReadTrainingSessionDto ToReadDto(TrainingSession trainingSession);
    //GET ALL
    public partial List<ReadTrainingSessionDto> ToReadDtoList(List<TrainingSession> trainingSessions);
    //PUT 
    [MapperIgnoreTarget(nameof(TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(TrainingSession.TrainerId))]
    [MapperIgnoreTarget(nameof(TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(TrainingSession.MemberId))]
    [MapperIgnoreTarget(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.Description))]
    [MapperIgnoreTarget(nameof(TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(TrainingSession.IsGroupSession))]
    public partial void UpdateEntity(UpdateTrainingSessionDto dto, TrainingSession trainingSession);
}