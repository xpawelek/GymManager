using GymManager.Models.DTOs.Member;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberSelfMessageMapper
{
    //POST
    [MapperIgnoreTarget(nameof(Entities.Message.Id))]
    [MapperIgnoreTarget(nameof(Entities.Message.MemberId))]
    [MapperIgnoreTarget(nameof(Entities.Message.Member))]
    [MapperIgnoreTarget(nameof(Entities.Message.Trainer))]
    public partial Entities.Message ToEntity(CreateMessageDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Message.MemberId))]
    [MapperIgnoreSource(nameof(Entities.Message.Member))]
    [MapperIgnoreSource(nameof(Entities.Message.Trainer))]
    public partial ReadSelfMessageDto ToReadDto(Entities.Message message);
    //GET ALL
    public partial List<ReadSelfMessageDto> ToReadDtoList(List<Entities.Message> messages);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Message.Id))]
    [MapperIgnoreTarget(nameof(Entities.Message.MemberId))]
    [MapperIgnoreTarget(nameof(Entities.Message.Member))]
    [MapperIgnoreTarget(nameof(Entities.Message.Trainer))]
    [MapperIgnoreTarget(nameof(Entities.Message.Date))]
    [MapperIgnoreTarget(nameof(Entities.Message.TrainerId))]
    public partial void UpdateEntity(UpdateMessageDto dto, Entities.Message message);
}