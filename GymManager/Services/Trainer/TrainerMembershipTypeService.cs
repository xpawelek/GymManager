using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerMembershipTypeService
    {
        private readonly GymDbContext _context;
        private readonly TrainerMembershipTypeMapper _mapper;

        public TrainerMembershipTypeService(
            GymDbContext context,
            TrainerMembershipTypeMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadMembershipTypeDto>> GetAllAsync()
        {
            var list = await _context.MembershipTypes
                .Where(mt => mt.IsVisible)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadMembershipTypeDto> GetByIdAsync(int id)
        {
            var entity = await _context.MembershipTypes.FindAsync(id);
            return _mapper.ToReadDto(entity!);
        }
    }
}
