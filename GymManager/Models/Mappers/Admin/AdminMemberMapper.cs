﻿using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminMemberMapper
{
    //POST
    [MapperIgnoreTarget(nameof(Entities.Member.UserId))]
    [MapperIgnoreTarget(nameof(Entities.Member.User))]
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    public partial Entities.Member ToEntity(CreateMemberDto dto);
    // GET ONE
    [MapProperty("User.Email", "Email")]
    [MapperIgnoreSource(nameof(Entities.Member.UserId))]
    [MapperIgnoreSource(nameof(Entities.Member.Email))]
    public partial ReadMemberDto ToReadDto(Entities.Member member);
    //GET ALL
    public partial List<ReadMemberDto> ToReadDtoList(List<Entities.Member> members);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Member.UserId))]
    [MapperIgnoreTarget(nameof(Entities.Member.User))]
    [MapperIgnoreTarget(nameof(Entities.Member.Id))]
    [MapperIgnoreTarget(nameof(Entities.Member.MembershipCardNumber))]
    public partial void UpdateEntity(UpdateMemberDto dto, Entities.Member member);
}