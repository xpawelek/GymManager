using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
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

        private async Task<int> GetCurrentTrainerIdAsync()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr);

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(t => t.UserId == userId.ToString());

            if (trainer == null)
                throw new Exception("Trainer not found");

            return trainer.Id;
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            var trainerId = await GetCurrentTrainerIdAsync();
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
                .Where(p => p.IsPublic)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadProgressPhotoDto> GetByIdAsync(int id)
        {
            var trainerId = await GetCurrentTrainerIdAsync();

            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null)
                return null!;

            var allowed = await _context.TrainerAssignments
                .AnyAsync(ta => ta.TrainerId == trainerId
                              && ta.MemberId == entity.MemberId
                              && ta.IsActive);

            if (!allowed)
                return null!;

            return _mapper.ToReadDto(entity);
        }
    }
}
