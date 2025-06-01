using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly TrainerEquipmentMapper _mapper;

        public TrainerEquipmentService(GymDbContext context, TrainerEquipmentMapper mapper)
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
