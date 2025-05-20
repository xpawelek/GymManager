using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberTrainerService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainerMapper _mapper;

        public MemberTrainerService(GymDbContext context, MemberTrainerMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainerDto>> GetAllAsync()
            => _mapper.ToReadDtoList(await _context.Trainers.ToListAsync());

        public async Task<ReadTrainerDto?> GetByIdAsync(int id)
        {
            var e = await _context.Trainers.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }
    }
}
