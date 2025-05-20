using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly AdminEquipmentMapper _mapper;

        public AdminEquipmentService(GymDbContext context, AdminEquipmentMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadEquipmentDto>> GetAllAsync()
        {
            var list = await _context.Equipments.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadEquipmentDto?> GetByIdAsync(int id)
        {
            var e = await _context.Equipments.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<ReadEquipmentDto> CreateAsync(CreateEquipmentDto dto)
        {
            var e = _mapper.ToEntity(dto);
            await _context.Equipments.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateEquipmentDto dto)
        {
            var e = await _context.Equipments.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Equipments.FindAsync(id);
            if (e == null) return false;
            _context.Equipments.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
