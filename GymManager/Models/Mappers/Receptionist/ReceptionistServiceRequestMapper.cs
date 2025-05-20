using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistServiceRequestMapper
{
    //rola receptionist - zglasza bledy, czyta co do zrobienia, robi update po zrobieniu, dostaniu informacji
    //POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(CreateServiceRequestDto.ImagePath))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
    // GET ONE
    public partial ReadServiceRequestDto ToReadDto(ServiceRequest serviceRequest);
    //GET ALL
    public partial List<ReadServiceRequestDto> ToReadDtoList(List<ServiceRequest> serviceRequests);
    //PUT 
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.ImagePath))]
    [MapperIgnoreSource(nameof(UpdateServiceRequestDto.Image))]
    public partial void UpdateEntity(UpdateServiceRequestDto dto, ServiceRequest serviceRequest);
}