using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerServiceRequestMapper
{
    // POST
    [MapperIgnoreTarget(nameof(ServiceRequest.Id))]
    [MapperIgnoreTarget(nameof(ServiceRequest.RequestDate))]
    [MapperIgnoreTarget(nameof(ServiceRequest.IsResolved))]
    public partial ServiceRequest ToEntity(CreateServiceRequestDto dto);
}
