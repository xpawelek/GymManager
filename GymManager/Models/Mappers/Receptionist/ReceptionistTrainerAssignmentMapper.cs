using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;
namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistTrainerAssignmentMapper
{
    //rola receptionist - przychodzi klient na recepcje i zapomnial jakiego mial trenera przypisanego - musi czytac
    // GET ONE
    [MapperIgnoreSource(nameof(TrainerAssignments.Member))]
    [MapperIgnoreSource(nameof(TrainerAssignments.Trainer))]
    public partial ReadTrainerAssignmentDto ToReadDto(TrainerAssignments trainerAssignments);
    //GET ALL
    public partial List<ReadTrainerAssignmentDto> ToReadDtoList(List<TrainerAssignments> trainerAssignments);
}