using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminEquipmentMapper
{
    //POST
    [MapperIgnoreTarget(nameof(Equipment.Id))]
    [MapperIgnoreTarget(nameof(Equipment.ServiceRequests))]
    public partial Equipment ToEntity(CreateEquipmentDto dto);
    // GET ONE
    public partial ReadEquipmentDto ToReadDto(Equipment equipment);
    //GET ALL
    public partial List<ReadEquipmentDto> ToReadDtoList(List<Equipment> equipments);
    //PUT 
    [MapperIgnoreTarget(nameof(Equipment.Id))]
    [MapperIgnoreTarget(nameof(Equipment.ServiceRequests))]
    public partial void UpdateEntity(UpdateEquipmentDto dto, Equipment equipment);
}