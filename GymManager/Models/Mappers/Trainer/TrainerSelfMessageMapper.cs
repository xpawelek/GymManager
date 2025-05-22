using Riok.Mapperly.Abstractions;
using GymManager.Models.DTOs.Trainer;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerSelfMessageMapper
{
    //POST
    [MapperIgnoreTarget(nameof(Entities.Message.Id))]
    [MapperIgnoreTarget(nameof(Entities.Message.Member))]
    [MapperIgnoreTarget(nameof(Entities.Message.Trainer))]
    [MapperIgnoreTarget(nameof(Entities.Message.TrainerId))]
    [MapperIgnoreTarget(nameof(Entities.Message.SentByMember))]
    public partial Entities.Message ToEntity(CreateSelfMessageDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(Entities.Message.Member))]
    [MapperIgnoreSource(nameof(Entities.Message.Trainer))]
    [MapperIgnoreSource(nameof(Entities.Message.TrainerId))]
    [MapperIgnoreSource(nameof(Entities.Message.SentByMember))]
    public partial ReadSelfMessageDto ToReadDto(Entities.Message message);
    //GET ALL
    public partial List<ReadSelfMessageDto> ToReadDtoList(List<Entities.Message> messages);
    //PUT 
    [MapperIgnoreTarget(nameof(Entities.Message.Id))]
    [MapperIgnoreTarget(nameof(Entities.Message.Member))]
    [MapperIgnoreTarget(nameof(Entities.Message.MemberId))]
    [MapperIgnoreTarget(nameof(Entities.Message.Trainer))]
    [MapperIgnoreTarget(nameof(Entities.Message.Date))]
    [MapperIgnoreTarget(nameof(Entities.Message.TrainerId))]
    [MapperIgnoreTarget(nameof(Entities.Message.SentByMember))]
    public partial void UpdateEntity(UpdateSelfMessageDto dto, Entities.Message message);
}