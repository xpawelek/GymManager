using Riok.Mapperly.Abstractions;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Entities;
namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerTrainingSessionMapper
{
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
    [MapperIgnoreTarget(nameof(TrainingSession.WorkoutNote))]
    [MapperIgnoreTarget(nameof(TrainingSession.TrainerId))]
    [MapperIgnoreTarget(nameof(TrainingSession.MemberId))]
    [MapperIgnoreTarget(nameof(TrainingSession.Id))]
    [MapperIgnoreTarget(nameof(TrainingSession.IsGroupSession))]
    [MapperIgnoreTarget(nameof(TrainingSession.DurationInMinutes))]
    [MapperIgnoreTarget(nameof(TrainingSession.StartTime))]
    public partial void UpdateEntity(UpdateTrainingSessionDto dto, TrainingSession trainingSession);
}