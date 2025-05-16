using GymManager.Models.DTOs.Member;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfMapper
{
    // GET OWN INFO
    public partial ReadSelfMemberDto ToReadDto(Entities.Member member);

    // UPDATE OWN INFO
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    public partial void UpdateEntity(UpdateSelfMemberDto dto, Entities.Member member);
}