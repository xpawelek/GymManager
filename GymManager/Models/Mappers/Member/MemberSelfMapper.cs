using GymManager.Shared.DTOs.Member;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfMapper
{
    // GET OWN INFO
    [MapProperty("User.Email", "Email")]
    public partial ReadSelfMemberDto ToReadDto(Entities.Member member);

    // UPDATE OWN INFO
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    [MapperIgnoreTarget(nameof(Entities.Member.Email))]
    [MapperIgnoreTarget(nameof(Entities.Member.User))]
    [MapperIgnoreTarget(nameof(Entities.Member.UserId))]
    
    public partial void UpdateEntity(UpdateSelfMemberDto dto, Entities.Member member);
}