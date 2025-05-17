using Riok.Mapperly.Abstractions;
using GymManager.Models.DTOs.Trainer;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerProfileMapper
{
    // GET ONE
    public partial ReadTrainerDto ToReadDto(Entities.Trainer trainer);
    //GET ALL
    public partial List<ReadTrainerDto> ToReadDtoList(List<Entities.Trainer> trainers);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Trainer.Id))]
    public partial void UpdateEntity(UpdateSelfTrainerDto dto, Entities.Trainer trainer);
}