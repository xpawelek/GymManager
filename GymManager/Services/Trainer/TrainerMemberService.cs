using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
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
            var assignments = await _context.TrainerAssignments
                .Where(ta => ta.TrainerId == trainerId && ta.IsActive)
                .Include(ta => ta.Member)
                .Select(ta => ta.Member)
                .ToListAsync();
            
            return _mapper.ToReadDtoList(assignments);
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
