using GymManager.Shared.DTOs.Member;
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
    [MapperIgnoreTarget(nameof(TrainerAssignments.IsActive))]
    public partial TrainerAssignments ToEntity(CreateTrainerAssignmentDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(TrainerAssignments.Member))]
    [MapperIgnoreSource(nameof(TrainerAssignments.MemberId))]
    [MapProperty("Trainer.FirstName", "TrainerFirstName")]
    [MapProperty("Trainer.LastName", "TrainerSecondName")]
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
    [MapperIgnoreTarget(nameof(TrainerAssignments.IsActive))]
    public partial void UpdateEntity(UpdateSelfTrainerAssignmentsDto dto, TrainerAssignments trainerAssignments);
}