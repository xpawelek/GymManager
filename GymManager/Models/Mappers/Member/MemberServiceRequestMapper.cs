using GymManager.Models.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberServiceRequestMapper
{
    //POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(CreateServiceRequestDto.Image))]
    [MapperIgnoreSource(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.Equipment))]
    [MapperIgnoreTarget(nameof(ServiceRequest.EquipmentId))]

    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);

}