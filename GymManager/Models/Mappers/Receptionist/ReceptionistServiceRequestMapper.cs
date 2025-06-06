using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistServiceRequestMapper
{
    // rola receptionist: zgłasza problemy, przegląda, aktualizuje
    // === POST ===
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);

    // === GET ONE ===
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);

    // === GET ALL ===
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);

    // === PATCH ===
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}
