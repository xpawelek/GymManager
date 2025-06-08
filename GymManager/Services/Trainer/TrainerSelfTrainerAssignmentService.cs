using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerSelfTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly TrainerSelfTrainerAssignmentMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerSelfTrainerAssignmentService(
            GymDbContext context,
            TrainerSelfTrainerAssignmentMapper mapper,
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
                throw new InvalidOperationException("Trainer not found");

            return trainer.Id;
        }


        public async Task<List<ReadSelfTrainerAssignmentDto>> GetAllAsync()
        {
            var tid = await GetCurrentTrainerIdAsync();

            var list = await _context.TrainerAssignments
                .Where(a => a.TrainerId == tid)
                .ToListAsync();

            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfTrainerAssignmentDto?> GetByIdAsync(int id)
        {
            var tid = await GetCurrentTrainerIdAsync();

            var entity = await _context.TrainerAssignments.FindAsync(id);
            if (entity == null || entity.TrainerId != tid)
                return null;

            return _mapper.ToReadDto(entity);
        }

        public async Task<List<ReadSelfTrainerAssignmentDto>> GetAllForMemberAsync(int memberId)
        {
            var trainerId = await GetCurrentTrainerIdAsync();

            var assignments = await _context.TrainerAssignments
                .Where(a => a.TrainerId == trainerId && a.MemberId == memberId)
                .ToListAsync();

            return _mapper.ToReadDtoList(assignments);
        }

    }
}
