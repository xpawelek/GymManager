using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminServiceRequestMapper
{
    // POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);

    // GET ONE
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);

    // GET ALL
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);

    // PUT
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}
