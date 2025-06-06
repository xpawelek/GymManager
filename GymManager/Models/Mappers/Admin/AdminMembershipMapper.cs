using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminMembershipMapper
{
    // POST: Create
    [MapperIgnoreTarget(nameof(Membership.Id))]
    [MapperIgnoreTarget(nameof(Membership.Member))]
    [MapperIgnoreTarget(nameof(Membership.MembershipType))]
    public partial Membership ToEntity(CreateMembershipDto dto);

    // PUT/PATCH: Update
    [MapperIgnoreTarget(nameof(Membership.Id))]
    [MapperIgnoreTarget(nameof(Membership.Member))]
    [MapperIgnoreTarget(nameof(Membership.MemberId))]
    [MapperIgnoreTarget(nameof(Membership.MembershipType))]
    public partial void UpdateEntity(UpdateMembershipDto dto, Membership membership);
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Membership.MemberId))]
    [MapperIgnoreSource(nameof(Entities.Membership.MembershipTypeId))]
    [MapProperty("Member.Id", "MemberId")]
    [MapProperty("MembershipType.Id", "MembershipTypeId")]
    [MapProperty("MembershipType.Name", "TypeName")]
    public partial ReadMembershipDto ToReadDto(Entities.Membership membership);
    //GET ALL
    public partial List<ReadMembershipDto> ToReadDtoList(List<Entities.Membership> memberships);
}