using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerMemberMapper
{
    // GET ONE
    public partial ReadMemberDto ToReadDto(Entities.Member member);
    //GET ALL
    public partial List<ReadMemberDto> ToReadDtoList(List<Entities.Member> members);
}