using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberEquipmentMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Equipment.Notes))]
    [MapperIgnoreSource(nameof(Entities.Equipment.ServiceRequests))]
    public partial ReadEquipmentDto ToReadDto(Equipment equipment);
    //GET ALL
    public partial List<ReadEquipmentDto> ToReadDtoList(List<Equipment> equipments);
}