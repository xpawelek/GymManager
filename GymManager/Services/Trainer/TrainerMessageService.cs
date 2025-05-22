using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerMessageService
    {
        private readonly GymDbContext _context;
        private readonly TrainerSelfMessageMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerMessageService(
            GymDbContext context,
            TrainerSelfMessageMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetTrainerId()
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

        public async Task<List<ReadSelfMessageDto>> GetAllAsync()
        {
            var tid = await GetTrainerId();
            var list = await _context.Messages
                .Where(m => m.TrainerId == tid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfMessageDto?> GetByIdAsync(int id)
        {
            var tid = await GetTrainerId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.TrainerId != tid) return null;
            return _mapper.ToReadDto(e);
        }

        public async Task<ReadSelfMessageDto> CreateAsync(CreateSelfMessageDto dto)
        {
            var tid = await GetTrainerId();
            var assignment = await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == dto.MemberId && a.TrainerId == tid && a.IsActive);
            if (!assignment)
                throw new InvalidOperationException("You are not connected to this member");

            var e = _mapper.ToEntity(dto);
            e.TrainerId = tid;
            e.SentByMember = false;
            await _context.Messages.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> UpdateAsync(int id, UpdateSelfMessageDto dto)
        {
            var tid = await GetTrainerId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.TrainerId != tid || e.SentByMember) return false;
            
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
