using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminMembershipTypeMapper
{
    //POST
    [MapperIgnoreTarget(nameof(MembershipType.Id))]
    [MapperIgnoreTarget(nameof(MembershipType.Memberships))]
    public partial MembershipType ToEntity(CreateMembershipTypeDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(MembershipType.Memberships))]
    public partial ReadMembershipTypeDto ToReadDto(MembershipType membershipType);
    //GET ALL
    public partial List<ReadMembershipTypeDto> ToReadDtoList(List<MembershipType> membershipTypes);
    
    //PUT 
    [MapperIgnoreTarget(nameof(MembershipType.Id))]
    [MapperIgnoreTarget(nameof(MembershipType.Memberships))]
    public partial void UpdateEntity(UpdateMembershipTypeDto dto, MembershipType membershipType);
}