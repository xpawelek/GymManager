using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainerService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerMapper _mapper;

        public AdminTrainerService(GymDbContext context, AdminTrainerMapper mapper)
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

        public async Task<ReadTrainerDto> CreateAsync(CreateTrainerDto dto)
        {
            var e = _mapper.ToEntity(dto);
            await _context.Trainers.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTrainerDto dto)
        {
            var e = await _context.Trainers.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Trainers.FindAsync(id);
            if (e == null) return false;
            _context.Trainers.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
