using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerAssignmentMapper _mapper;

        public AdminTrainerAssignmentService(
            GymDbContext context,
            AdminTrainerAssignmentMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainerAssignmentDto>> GetAllAsync()
        {
            var list = await _context.TrainerAssignments.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainerAssignmentDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<ReadTrainerAssignmentDto> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            var e = _mapper.ToEntity(dto);
            await _context.TrainerAssignments.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainerAssignmentsDto dto)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            if (e == null) return false;
            _context.TrainerAssignments.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
