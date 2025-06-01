using GymManager.Shared.DTOs.Admin;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminMessageMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Message.Member))]
    [MapperIgnoreSource(nameof(Entities.Message.Trainer))]
    [MapperIgnoreSource(nameof(Entities.Message.SentByMember))]
    public partial ReadMessageDto ToReadDto(Entities.Message message);
    //GET ALL
    public partial List<ReadMessageDto> ToReadDtoList(List<Entities.Message> messages);
}