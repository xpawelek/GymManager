using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerSelfTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly TrainerSelfTrainerAssignmentMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<TrainerSelfTrainerAssignmentService> _logger;

        public TrainerSelfTrainerAssignmentService(
            GymDbContext context,
            TrainerSelfTrainerAssignmentMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TrainerSelfTrainerAssignmentService> logger)
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
                    throw new InvalidOperationException("Trainer not found");

                return trainer.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get current trainer ID");
                throw;
            }
        }

        public async Task<List<ReadSelfTrainerAssignmentDto>> GetAllAsync()
        {
            try
            {
                var tid = await GetCurrentTrainerIdAsync();

                var list = await _context.TrainerAssignments
                    .Where(a => a.TrainerId == tid)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all trainer assignments");
                throw;
            }
        }

        public async Task<ReadSelfTrainerAssignmentDto?> GetByIdAsync(int id)
        {
            try
            {
                var tid = await GetCurrentTrainerIdAsync();

                var entity = await _context.TrainerAssignments.FindAsync(id);
                if (entity == null || entity.TrainerId != tid)
                    return null;

                return _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get assignment by ID: {id}");
                throw;
            }
        }

        public async Task<List<ReadSelfTrainerAssignmentDto>> GetAllForMemberAsync(int memberId)
        {
            try
            {
                var trainerId = await GetCurrentTrainerIdAsync();

                var assignments = await _context.TrainerAssignments
                    .Where(a => a.TrainerId == trainerId && a.MemberId == memberId)
                    .ToListAsync();

                return _mapper.ToReadDtoList(assignments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get assignments for member ID: {memberId}");
                throw;
            }
        }
    }
}
