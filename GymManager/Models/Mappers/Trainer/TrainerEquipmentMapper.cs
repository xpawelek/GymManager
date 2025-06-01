using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerEquipmentMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Equipment.Notes))]
    [MapperIgnoreSource(nameof(Entities.Equipment.ServiceRequests))]
    public partial ReadEquipmentDto ToReadDto(Equipment equipment);
    //GET ALL
    public partial List<ReadEquipmentDto> ToReadDtoList(List<Equipment> equipments);
}