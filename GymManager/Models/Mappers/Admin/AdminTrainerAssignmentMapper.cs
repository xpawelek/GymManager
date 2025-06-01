using GymManager.Shared.DTOs.Admin;
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
    [MapperIgnoreTarget(nameof(TrainerAssignments.IsActive))]
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
    [MapperIgnoreTarget(nameof(TrainerAssignments.MemberId))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.StartDate))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.EndDate))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.IsActive))]
    public partial void UpdateEntity(UpdateTrainerAssignmentsDto dto, TrainerAssignments trainerAssignments);
}