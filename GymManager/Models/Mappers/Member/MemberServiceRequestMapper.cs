using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberServiceRequestMapper
{
    //POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.IsResolved))]

    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
}