using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerSelfWorkoutNoteService
    {
        private readonly GymDbContext _context;
        private readonly TrainerWorkoutNoteMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerSelfWorkoutNoteService(
            GymDbContext context,
            TrainerWorkoutNoteMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private int GetCurrentTrainerId() =>
            int.Parse(_httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<List<ReadSelfWorkoutNoteDto>> GetAllAsync()
        {
            var tid = GetCurrentTrainerId();
            var list = await _context.WorkoutNotes
                .Where(n => n.TrainerId == tid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfWorkoutNoteDto?> GetByIdAsync(int id)
        {
            var tid = GetCurrentTrainerId();
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.TrainerId != tid) return null;
            return _mapper.ToReadDto(e);
        }

        public async Task<ReadSelfWorkoutNoteDto> CreateAsync(CreateSelfWorkoutNote dto)
        {
            dto.TrainerId = GetCurrentTrainerId();
            var e = _mapper.ToEntity(dto);
            await _context.WorkoutNotes.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateSelfWorkoutNoteDto dto)
        {
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.TrainerId != GetCurrentTrainerId()) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.TrainerId != GetCurrentTrainerId()) return false;
            _context.WorkoutNotes.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
