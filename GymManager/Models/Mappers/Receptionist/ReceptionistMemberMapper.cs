using GymManager.Models.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;
namespace GymManager.Models.Mappers.Receptionist;

[Mapper]
public partial class ReceptionistMemberMapper
{
    //rola receptionist - moze wyszukiwac uzytkownikow, moze edytowac np dane membera - bo przyeszedl na recepcje po to
    // moze dodawac nowych - nie zakladaja konta online
    //POST
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    public partial Entities.Member ToEntity(CreateMemberDto dto);
    // GET ONE
    public partial ReadMemberDto ToReadDto(Entities.Member member);
    //GET ALL
    public partial List<ReadMemberDto> ToReadDtoList(List<Entities.Member> members);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    public partial void UpdateEntity(UpdateMemberDto dto, Entities.Member member);
}