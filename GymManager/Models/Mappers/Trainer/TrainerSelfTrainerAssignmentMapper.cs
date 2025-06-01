using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerSelfTrainerAssignmentMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(TrainerAssignments.Member))]
    [MapperIgnoreSource(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreSource(nameof(TrainerAssignments.TrainerId))]
    public partial ReadSelfTrainerAssignmentDto ToReadDto(TrainerAssignments trainerAssignments);
    //GET ALL
    public partial List<ReadSelfTrainerAssignmentDto> ToReadDtoList(List<TrainerAssignments> trainerAssignments);
}