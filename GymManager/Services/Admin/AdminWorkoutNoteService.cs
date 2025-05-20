using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminWorkoutNoteService
    {
        private readonly GymDbContext _context;
        private readonly AdminWorkoutNoteMapper _mapper;

        public AdminWorkoutNoteService(
            GymDbContext context,
            AdminWorkoutNoteMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadWorkoutNoteDto>> GetAllAsync()
        {
            var list = await _context.WorkoutNotes.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadWorkoutNoteDto?> GetByIdAsync(int id)
        {
            var e = await _context.WorkoutNotes.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }
    }
}
