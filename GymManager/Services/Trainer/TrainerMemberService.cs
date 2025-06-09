using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerMemberService
    {
        private readonly GymDbContext _context;
        private readonly TrainerMemberMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerMemberService(
            GymDbContext context,
            TrainerMemberMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetCurrentTrainerId()
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

        public async Task<List<ReadMemberDto>> GetAllAsync()
        {
            var trainerId = await GetCurrentTrainerId()!;
            var memberIds = await _context.TrainerAssignments
                .Where(a => a.TrainerId == trainerId)
                .Select(a => a.MemberId)
                .Distinct()
                .ToListAsync();

            var members = await _context.Members
                .Where(m => memberIds.Contains(m.Id))
                .Include(m => m.User)
                .ToListAsync();

            return _mapper.ToReadDtoList(members);
        }

        public async Task<ReadMemberDto> GetByIdAsync(int memberId)
        {
            var trainerId = await GetCurrentTrainerId()!;
            var allowed = await _context.TrainerAssignments
                .AnyAsync(ta => ta.TrainerId == trainerId
                                && ta.MemberId == memberId
                                && ta.IsActive);
            if (!allowed) return null!;

            var entity = await _context.Members.FindAsync(memberId);
            return _mapper.ToReadDto(entity!);
        }
    }
}
