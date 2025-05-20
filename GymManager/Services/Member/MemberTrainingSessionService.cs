using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainingSessionMapper _mapper;

        public MemberTrainingSessionService(
            GymDbContext context,
            MemberTrainingSessionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            var list = await _context.TrainingSessions
                .Where(ts => ts.IsGroupSession || ts.MemberId == null)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return null;
            return _mapper.ToReadDto(e);
        }
    }
}
