using GymManager.Models.DTOs.Member;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfMembershipMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Membership.MemberId))]
    [MapperIgnoreSource(nameof(Entities.Membership.MembershipTypeId))]
    [MapperIgnoreSource(nameof(Entities.Membership.Member))]
    public partial ReadSelfMembershipDto ToReadDto(Entities.Membership membership);
    
    // UPDATE OWN MEMBERSHIP - PUT
    [MapperIgnoreTarget(nameof(Entities.Membership.Member))]
    [MapperIgnoreTarget(nameof(Entities.Membership.Id))]
    [MapperIgnoreTarget(nameof(Entities.Membership.MemberId))]
    [MapperIgnoreTarget(nameof(Entities.Membership.MembershipType))]
    public partial void UpdateEntity(UpdateMembershipDto dto, Entities.Membership membership);
}