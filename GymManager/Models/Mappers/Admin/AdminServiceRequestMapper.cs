using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminServiceRequestMapper
{
    //POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(CreateServiceRequestDto.ImagePath))]
    [MapperIgnoreTarget(nameof(ServiceRequest.Equipment))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(ServiceRequest.Equipment))]
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);
    //GET ALL
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);
    //PUT 
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(UpdateServiceRequestDto.Image))]
    [MapperIgnoreTarget(nameof(ServiceRequest.Equipment))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}