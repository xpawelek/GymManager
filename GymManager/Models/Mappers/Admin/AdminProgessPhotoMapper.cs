using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace GymManager.Models.Mappers.Admin;

[Mapper]
public partial class AdminProgessPhotoMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(ProgressPhoto.Member))]
    public partial ReadProgressPhotoDto ToReadDto(ProgressPhoto progressPhoto);
    //GET ALL
    public partial List<ReadProgressPhotoDto> ToReadDtoList(List<ProgressPhoto> progressPhotos);
    
    //PUT 
    [MapperIgnoreTarget(nameof(ProgressPhoto.Id))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.MemberId))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.Date))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.Member))]
    [MapperIgnoreTarget(nameof(ProgressPhoto.ImagePath))]
    public partial void UpdateEntity(UpdateProgressPhotoDto dto, ProgressPhoto progressPhoto);
}