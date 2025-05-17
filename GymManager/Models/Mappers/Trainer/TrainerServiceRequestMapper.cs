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
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
    // GET ONE
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);
    //GET ALL
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);
    //PUT 
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(DTOs.Trainer.UpdateServiceRequestDto.Image))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}