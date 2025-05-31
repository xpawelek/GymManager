using Riok.Mapperly.Abstractions;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerServiceRequestMapper
{
    //POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(DTOs.Trainer.CreateServiceRequestDto.ImagePath))]
    [MapperIgnoreSource(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.Equipment))]
    [MapperIgnoreTarget(nameof(ServiceRequest.EquipmentId))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(ServiceRequest.Equipment))]
    [MapperIgnoreSource(nameof(ServiceRequest.EquipmentId))]
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);
    //GET ALL
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);
    //PUT 
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(DTOs.Trainer.UpdateServiceRequestDto.Image))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.Equipment))]
    [MapperIgnoreTarget(nameof(ServiceRequest.EquipmentId))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}