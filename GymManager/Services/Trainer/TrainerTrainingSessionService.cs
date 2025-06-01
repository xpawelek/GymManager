using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly TrainerTrainingSessionMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerTrainingSessionService(
            GymDbContext context,
            TrainerTrainingSessionMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetCurrentTrainerIdAsync()
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

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            var trainerId = await GetCurrentTrainerIdAsync();
            var sessions = await _context.TrainingSessions
                .Where(ts => ts.TrainerId == trainerId)
                .ToListAsync();

            return _mapper.ToReadDtoList(sessions);
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var trainerId = await GetCurrentTrainerIdAsync();
            var entity = await _context.TrainingSessions.FindAsync(id);
            if (entity == null || entity.TrainerId != trainerId)
                return null;

            return _mapper.ToReadDto(entity);
        }
    }
}
