using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly MemberEquipmentMapper _mapper;

        public MemberEquipmentService(GymDbContext context, MemberEquipmentMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadEquipmentDto>> GetAllAsync()
            => _mapper.ToReadDtoList(await _context.Equipments.ToListAsync());

        public async Task<ReadEquipmentDto?> GetByIdAsync(int id)
        {
            var e = await _context.Equipments.FindAsync(id);
            return e == null ? null : _mapper.ToReadDto(e);
        }
    }
}
