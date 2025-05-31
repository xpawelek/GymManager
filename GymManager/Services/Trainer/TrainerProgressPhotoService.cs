using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly TrainerProgressPhotoMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerProgressPhotoService(
            GymDbContext context,
            TrainerProgressPhotoMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private int? GetCurrentTrainerId()
        {
            var id = _httpContext.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(id, out var tid) ? tid : null;
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            var trainerId = GetCurrentTrainerId()!.Value;
            var memberIds = await _context.TrainerAssignments
                .Where(ta => ta.TrainerId == trainerId && ta.IsActive)
                .Select(ta => ta.MemberId)
                .ToListAsync();

            var list = await _context.ProgressPhotos
                .Where(p => p.IsPublic && memberIds.Contains(p.MemberId))
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }
        
        public async Task<List<ReadProgressPhotoDto>> GetAllPublic()
        {
            var list = await _context.ProgressPhotos
                .Where(p => p.IsPublic == true)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadProgressPhotoDto> GetByIdAsync(int id)
        {
            var trainerId = GetCurrentTrainerId()!.Value;
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null) return null!;
            var allowed = await _context.TrainerAssignments
                .AnyAsync(ta => ta.TrainerId == trainerId
                              && ta.MemberId == entity.MemberId
                              && ta.IsActive);
            if (!allowed) return null!;
            return _mapper.ToReadDto(entity);
        }
    }
}
