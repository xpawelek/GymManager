using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
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

        private int GetCurrentTrainerId() =>
            int.Parse(_httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            var tid = GetCurrentTrainerId();
            var list = await _context.TrainingSessions
                .Where(ts => ts.TrainerId == tid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var tid = GetCurrentTrainerId();
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null || e.TrainerId != tid) return null;
            return _mapper.ToReadDto(e);
        }
    }
}
