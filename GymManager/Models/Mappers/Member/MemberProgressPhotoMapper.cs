using GymManager.Models.DTOs.Member;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Member;

[Mapper]
public partial class MemberProgressPhotoMapper
{
    //POST
    [MapperIgnoreTarget(nameof(ProgressPhoto.Id))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.Member))]
    public partial ProgressPhoto ToEntity(CreateProgressPhotoDto dto);
    // GET ONE
    [MapperIgnoreSource(nameof(ProgressPhoto.Member))]
    public partial ReadProgressPhotoDto ToReadDto(ProgressPhoto progress);
    //GET ALL
    public partial List<ReadProgressPhotoDto> ToReadDtoList(List<ProgressPhoto> progressPhotos);
    //PUT 
    [MapperIgnoreTarget(nameof(ProgressPhoto.Id))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.Member))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.Date))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.MemberId))]
    public partial void UpdateEntity(UpdateProgressPhotoDto dto, ProgressPhoto progressPhoto);
}