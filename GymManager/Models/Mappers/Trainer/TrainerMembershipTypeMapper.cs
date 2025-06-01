using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerMembershipTypeMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(MembershipType.Memberships))]
    [MapperIgnoreSource(nameof(MembershipType.IsVisible))]
    public partial ReadMembershipTypeDto ToReadDto(MembershipType membershipType);
    //GET ALL
    public partial List<ReadMembershipTypeDto> ToReadDtoList(List<MembershipType> membershipTypes);
}