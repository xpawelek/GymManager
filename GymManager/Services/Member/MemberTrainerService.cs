using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberTrainerService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainerMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberTrainerService(GymDbContext context, MemberTrainerMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetCurrentMemberIdAsync()
        {
            var userIdStr = _httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdStr))
                throw new InvalidOperationException("User ID not found in token");

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == userIdStr);

            if (member == null)
                throw new InvalidOperationException("Member not found");

            return member.Id;
        }

        public async Task<ReadTrainerDto?> GetAssignedTrainerAsync()
        {
            var memberId = await GetCurrentMemberIdAsync();

            var assignment = await _context.TrainerAssignments
                .Include(a => a.Trainer)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(a => a.MemberId == memberId && a.IsActive);

            return assignment == null ? null : _mapper.ToReadDto(assignment.Trainer);
        }

        public async Task<List<ReadTrainerDto>> GetAllContactedAsync()
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

        public async Task<List<ReadTrainerDto>> GetAllAsync()
            => _mapper.ToReadDtoList(await _context.Trainers.Include(t => t.User).ToListAsync());

        public async Task<ReadTrainerDto?> GetByIdAsync(int id)
        {
            var e = await _context.Trainers.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            return e is null ? null : _mapper.ToReadDto(e);
        }
    }
}
