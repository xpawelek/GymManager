using Riok.Mapperly.Abstractions;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Entities;

namespace GymManager.Models.Mappers.Trainer;

[Mapper]
public partial class TrainerProgressPhotoMapper
{
    // GET ONE
    [MapperIgnoreSource(nameof(ProgressPhoto.Member))]
    public partial ReadProgressPhotoDto ToReadDto(ProgressPhoto progressPhoto);
    //GET ALL
    public partial List<ReadProgressPhotoDto> ToReadDtoList(List<ProgressPhoto> progressPhotos);
    
}