using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberMembershipTypeMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(MembershipType.Memberships))]
    [MapperIgnoreSource(nameof(MembershipType.IsVisible))]
    public partial ReadMembershipTypeDto ToReadDto(MembershipType membershipType);
    //GET ALL
    public partial List<ReadMembershipTypeDto> ToReadDtoList(List<MembershipType> membershipTypes);
}