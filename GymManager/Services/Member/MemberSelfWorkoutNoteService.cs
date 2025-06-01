using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberSelfWorkoutNoteService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfWorkoutNoteMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberSelfWorkoutNoteService(
            GymDbContext context,
            MemberSelfWorkoutNoteMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetCurrentMemberId()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr); 

            var member = await _context.Members
                .FirstOrDefaultAsync(t => t.UserId == userId.ToString());

            if (member == null)
                throw new Exception("Member not found");

            return member.Id;
        }

        public async Task<List<ReadSelfWorkoutNoteDto>> GetAllAsync()
        {
            var mid = await GetCurrentMemberId();
            var list = await _context.WorkoutNotes
                .Where(n => n.MemberId == mid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfWorkoutNoteDto?> GetByIdAsync(int id)
        {
            var mid = await GetCurrentMemberId();
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.MemberId != mid) return null;
            return _mapper.ToReadDto(e);
        }
    }
}
