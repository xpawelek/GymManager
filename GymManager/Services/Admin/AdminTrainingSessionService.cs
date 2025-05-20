using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainingSessionMapper _mapper;

        public AdminTrainingSessionService(
            GymDbContext context,
            AdminTrainingSessionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            var list = await _context.TrainingSessions.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<ReadTrainingSessionDto> CreateAsync(CreateTrainingSessionDto dto)
        {
            var e = _mapper.ToEntity(dto);
            await _context.TrainingSessions.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainingSessionDto dto)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return false;
            _context.TrainingSessions.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
