using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
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

        private int GetCurrentMemberId() =>
            int.Parse(_httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<List<ReadSelfWorkoutNote>> GetAllAsync()
        {
            var mid = GetCurrentMemberId();
            var list = await _context.WorkoutNotes
                .Where(n => n.MemberId == mid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfWorkoutNote?> GetByIdAsync(int id)
        {
            var mid = GetCurrentMemberId();
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.MemberId != mid) return null;
            return _mapper.ToReadDto(e);
        }
    }
}
