using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminWorkoutNoteService
    {
        private readonly GymDbContext _context;
        private readonly AdminWorkoutNoteMapper _mapper;
        private readonly ILogger<AdminWorkoutNoteService> _logger;

        public AdminWorkoutNoteService(
            GymDbContext context,
            AdminWorkoutNoteMapper mapper,
            ILogger<AdminWorkoutNoteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadWorkoutNoteDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.WorkoutNotes.ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all workout notes.");
                return new List<ReadWorkoutNoteDto>();
            }
        }

        public async Task<ReadWorkoutNoteDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.WorkoutNotes.FindAsync(id);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving workout note with ID {Id}.", id);
                return null;
            }
        }
    }
}
