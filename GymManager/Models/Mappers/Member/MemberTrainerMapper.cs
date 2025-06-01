using GymManager.Shared.DTOs.Member;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberTrainerMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Trainer.Email))]
    [MapperIgnoreSource(nameof(Entities.Trainer.PhoneNumber))]
    public partial ReadTrainerDto ToReadDto(Entities.Trainer trainer);
    //GET ALL
    public partial List<ReadTrainerDto> ToReadDtoList(List<Entities.Trainer> trainers);
}