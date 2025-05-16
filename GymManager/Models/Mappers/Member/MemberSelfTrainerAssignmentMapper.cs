using GymManager.Models.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfTrainerAssignmentMapper
{
    //POST
    [MapperIgnoreTarget(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Id))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.MemberId))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.StartDate))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Member))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.EndDate))]
    public partial TrainerAssignments ToEntity(CreateTrainerAssignmentDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreSource(nameof(TrainerAssignments.Member))]
    [MapperIgnoreSource(nameof(TrainerAssignments.MemberId))]
    public partial ReadSelfTrainerAssignmentDto ToReadDto(TrainerAssignments trainerAssignments);
    //GET ALL
    public partial List<ReadSelfTrainerAssignmentDto> ToReadDtoList(List<TrainerAssignments> trainerAssignments);
    //PUT 
    [MapperIgnoreTarget(nameof(TrainerAssignments.Trainer))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Id))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.MemberId))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.StartDate))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.Member))]
    [MapperIgnoreTarget(nameof(TrainerAssignments.EndDate))]
    public partial void UpdateEntity(UpdateSelfTrainerAssignmentsDto dto, TrainerAssignments trainerAssignments);
}