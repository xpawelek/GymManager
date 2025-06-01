using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistTrainingSessionMapper
{
    ////rola receptionist - przychodiz ktos i pyta jakie sa dzisiaj treningi grupowe - musi czytac 
    // GET ONE
    [MapperIgnoreSource(nameof(TrainingSession.Trainer))]
    [MapperIgnoreSource(nameof(TrainingSession.Member))]
    [MapperIgnoreSource(nameof(TrainingSession.WorkoutNote))]
    public partial ReadTrainingSessionDto ToReadDto(TrainingSession trainingSession);
    //GET ALL
    public partial List<ReadTrainingSessionDto> ToReadDtoList(List<TrainingSession> trainingSessions);
}