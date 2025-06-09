using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberTrainerService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainerMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberTrainerService> _logger;

        public MemberTrainerService(
            GymDbContext context,
            MemberTrainerMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberTrainerService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetCurrentMemberIdAsync()
        {
            var userIdStr = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
                throw new InvalidOperationException("User ID not found in token");

            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userIdStr);
            if (member == null)
                throw new InvalidOperationException("Member not found");

            return member.Id;
        }

        public async Task<ReadTrainerDto?> GetAssignedTrainerAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberIdAsync();

                var assignment = await _context.TrainerAssignments
                    .Include(a => a.Trainer)
                    .ThenInclude(t => t.User)
                    .FirstOrDefaultAsync(a => a.MemberId == memberId && a.IsActive);

                return assignment == null ? null : _mapper.ToReadDto(assignment.Trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting assigned trainer.");
                return null;
            }
        }

        public async Task<List<ReadTrainerDto>> GetAllContactedAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberIdAsync();

                var trainerIds = await _context.TrainerAssignments
                    .Where(a => a.MemberId == memberId)
                    .Select(a => a.TrainerId)
                    .Distinct()
                    .ToListAsync();

                var trainers = await _context.Trainers
                    .Where(t => trainerIds.Contains(t.Id))
                    .Include(t => t.User)
                    .ToListAsync();

                return _mapper.ToReadDtoList(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contacted trainers.");
                return new();
            }
        }

        public async Task<List<ReadTrainerDto>> GetAllAsync()
        {
            try
            {
                var trainers = await _context.Trainers.Include(t => t.User).ToListAsync();
                return _mapper.ToReadDtoList(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all trainers.");
                return new();
            }
        }

        public async Task<ReadTrainerDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.Trainers.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting trainer by id: {Id}", id);
                return null;
            }
        }
    }
}
