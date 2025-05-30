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

        private async Task<int> GetCurrentTrainerId()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr); 

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(t => t.UserId == userId.ToString());

            if (trainer == null)
                throw new Exception("Trainer not found");

            return trainer.Id;
        }

        public async Task<List<ReadSelfWorkoutNoteDto>> GetAllAsync()
        {
            var tid = await GetCurrentTrainerId();
            var list = await _context.WorkoutNotes
                .Where(n => n.TrainerId == tid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<List<ReadSelfWorkoutNoteDto?>> GetByIdAsync(int memberId)
        {
            var tid = await GetCurrentTrainerId();
            var assignment = await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == memberId && a.TrainerId == tid && a.IsActive);

            if (!assignment)
                throw new InvalidOperationException("You are not connected to this member");

            var notes = await _context.WorkoutNotes
                .Where(n => n.MemberId == memberId)
                .OrderByDescending(n => n.WorkoutStartTime)
                .ToListAsync();

            return _mapper.ToReadDtoList(notes);
        }

        //na razie skip create- automatycznie
        /*
        public async Task<ReadSelfWorkoutNoteDto> CreateAsync(CreateSelfWorkoutNote dto)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == userId);
            if (trainer == null)
                throw new ApplicationException("Trainer not found");
            
            var e = _mapper.ToEntity(dto);
            e.TrainerId = trainer.Id;
            await _context.WorkoutNotes.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }
        */
        public async Task<bool> PatchAsync(int id, UpdateSelfWorkoutNoteDto dto)
        {
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.TrainerId != await GetCurrentTrainerId()) return false;
            
            if(dto.WorkoutInfo == null) dto.WorkoutInfo = e.WorkoutInfo;
            if(dto.CurrentHeight == null) dto.CurrentHeight = e.CurrentHeight;
            if(dto.CurrentWeight == null) dto.CurrentWeight = e.CurrentWeight; 
            
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        //delete tez skip - usuwanie sesji treningowej - to tez usuniecie przypisanej notki
        /*
        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.WorkoutNotes.FindAsync(id);
            if (e == null || e.TrainerId != await GetCurrentTrainerId()) return false;
            _context.WorkoutNotes.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        */
    }
}
