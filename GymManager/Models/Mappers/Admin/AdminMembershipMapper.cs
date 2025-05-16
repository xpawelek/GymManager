using GymManager.Models.DTOs.Admin;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminMembershipMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Membership.MemberId))]
    [MapperIgnoreSource(nameof(Entities.Membership.MembershipTypeId))]
    [MapProperty("Member.Id", "MemberId")]
    [MapProperty("MembershipType.Id", "MembershipTypeId")]
    public partial ReadMembershipDto ToReadDto(Entities.Membership membership);
    //GET ALL
    public partial List<ReadMembershipDto> ToReadDtoList(List<Entities.Membership> memberships);
}