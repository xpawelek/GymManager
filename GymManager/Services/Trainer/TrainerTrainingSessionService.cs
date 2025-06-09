using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly TrainerTrainingSessionMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<TrainerTrainingSessionService> _logger;

        public TrainerTrainingSessionService(
            GymDbContext context,
            TrainerTrainingSessionMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TrainerTrainingSessionService> logger)
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
                var userIdStr = _httpContext.HttpContext?
                    .User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdStr))
                    throw new InvalidOperationException("UserId not found in token");

                var trainer = await _context.Trainers
                    .FirstOrDefaultAsync(t => t.UserId == userIdStr);

                if (trainer == null)
                    throw new InvalidOperationException("Trainer not found for current user");

                return trainer.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get current trainer ID");
                throw;
            }
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();

                var sessions = await _context.TrainingSessions
                    .Where(ts => ts.TrainerId == trainerId)
                    .ToListAsync();

                return _mapper.ToReadDtoList(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all training sessions");
                throw;
            }
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();
                var entity = await _context.TrainingSessions.FindAsync(id);

                if (entity == null || entity.TrainerId != trainerId)
                    return null;

                return _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve training session by ID: {id}");
                throw;
            }
        }
    }
}
