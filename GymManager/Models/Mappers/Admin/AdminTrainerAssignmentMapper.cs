using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminTrainerAssignmentMapper
{
    //POST
    [MapperIgnoreTarget(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Id))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Member))]
    public partial TrainerAssignments ToEntity(CreateTrainerAssignmentDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(TrainerAssignments.Member))]
    [MapperIgnoreSource(nameof(TrainerAssignments.Trainer))]
    public partial ReadTrainerAssignmentDto ToReadDto(TrainerAssignments trainerAssignments);
    //GET ALL
    public partial List<ReadTrainerAssignmentDto> ToReadDtoList(List<TrainerAssignments> trainerAssignments);
    //PUT 
    [MapperIgnoreTarget(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Id))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Member))]
    public partial void UpdateEntity(UpdateTrainerAssignmentsDto dto, TrainerAssignments trainerAssignments);
}