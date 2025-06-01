using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminTrainerMapper
{
    //POST
    [MapperIgnoreTarget(nameof(Entities.Trainer.Id))]
    public partial Entities.Trainer ToEntity(CreateTrainerDto dto);
    // GET ONE
    public partial ReadTrainerDto ToReadDto(Entities.Trainer trainer);
    //GET ALL
    public partial List<ReadTrainerDto> ToReadDtoList(List<Entities.Trainer> trainers);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Trainer.Id))]
    public partial void UpdateEntity(UpdateTrainerDto dto, Entities.Trainer trainer);
}