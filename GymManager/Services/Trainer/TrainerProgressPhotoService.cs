using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly TrainerProgressPhotoMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<TrainerProgressPhotoService> _logger;

        public TrainerProgressPhotoService(
            GymDbContext context,
            TrainerProgressPhotoMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TrainerProgressPhotoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetCurrentTrainerIdAsync()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get current trainer ID");
                throw;
            }
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();

                var memberIds = await _context.TrainerAssignments
                    .Where(ta => ta.TrainerId == trainerId)
                    .Select(ta => ta.MemberId)
                    .Distinct()
                    .ToListAsync();

                var list = await _context.ProgressPhotos
                    .Where(p => p.IsPublic || memberIds.Contains(p.MemberId))
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all progress photos");
                throw;
            }
        }

        public async Task<List<ReadProgressPhotoDto>> GetAssignedMembersPhotosAsync()
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();

                var memberIds = await _context.TrainerAssignments
                    .Where(ta => ta.TrainerId == trainerId)
                    .Select(ta => ta.MemberId)
                    .Distinct()
                    .ToListAsync();

                var photos = await _context.ProgressPhotos
                    .Where(p => memberIds.Contains(p.MemberId))
                    .ToListAsync();

                return _mapper.ToReadDtoList(photos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get assigned members' photos");
                throw;
            }
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllPublic()
        {
            try
            {
                var list = await _context.ProgressPhotos
                    .Where(p => p.IsPublic)
                    .ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get public progress photos");
                throw;
            }
        }

        public async Task<ReadProgressPhotoDto> GetByIdAsync(int id)
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();

                var entity = await _context.ProgressPhotos.FindAsync(id);
                if (entity == null)
                    return null!;

                var allowed = await _context.TrainerAssignments
                    .AnyAsync(ta => ta.TrainerId == trainerId && ta.MemberId == entity.MemberId);

                if (!allowed)
                    return null!;

                return _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get progress photo by ID: {id}");
                throw;
            }
        }
    }
}
