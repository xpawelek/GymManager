using Riok.Mapperly.Abstractions;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistMembershipTypeMapper
{
    //member chce aby recepcjonoista powiedzial mu jakie sa karnety - musi czytac
    // GET ONE
    [MapperIgnoreSource(nameof(MembershipType.Memberships))]
    public partial ReadMembershipTypeDto ToReadDto(MembershipType membershipType);
    //GET ALL
    public partial List<ReadMembershipTypeDto> ToReadDtoList(List<MembershipType> membershipTypes);
}