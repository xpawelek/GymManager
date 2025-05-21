using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistMembershipMapper
{
    //rola receptionist - nowy czlonek kupuje czlonkostwo, ktos przyszedl na recepcje i chce aby sprawdzono my
    // na ile ma jeszcze karnet, ktos chce zeby mu zmodyfikowano subskrypcje etc
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
    public partial ReadMembershipDto ToReadDto(Entities.Membership membership);
    //GET ALL
    public partial List<ReadMembershipDto> ToReadDtoList(List<Entities.Membership> memberships);
}