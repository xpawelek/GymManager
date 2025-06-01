using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberTrainingSessionMapper
{
    //POST

    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.WorkoutNote))]
    public partial TrainingSession ToEntity(CreateTrainingSessionDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.TrainingSession.WorkoutNote))]
    [MapperIgnoreSource(nameof(Entities.TrainingSession.Trainer))]
    [MapperIgnoreSource(nameof(Entities.TrainingSession.Member))]
    public partial ReadTrainingSessionDto ToReadDto(TrainingSession trainingSession);
    //GET ALL
    public partial List<ReadTrainingSessionDto> ToReadDtoList(List<TrainingSession> trainingSessions);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Trainer))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Member))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.TrainerId))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.Description))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.IsGroupSession))]
    [MapperIgnoreTarget(nameof(Entities.TrainingSession.MemberId))]
    public partial void UpdateEntity(UpdateTrainingSessionDto dto, TrainingSession trainingSession);
}