using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerMessageService
    {
        private readonly GymDbContext _context;
        private readonly TrainerSelfMessageMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<TrainerMessageService> _logger;

        public TrainerMessageService(
            GymDbContext context,
            TrainerSelfMessageMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<TrainerMessageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetTrainerId()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get trainer ID");
                throw;
            }
        }

        public async Task<List<ReadSelfMessageDto>> GetAllAsync()
        {
            try
            {
                var tid = await GetTrainerId();
                var list = await _context.Messages
                    .Where(m => m.TrainerId == tid)
                    .ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get messages for trainer");
                throw;
            }
        }

        public async Task<ReadSelfMessageDto?> GetByIdAsync(int id)
        {
            try
            {
                var tid = await GetTrainerId();
                var e = await _context.Messages.FindAsync(id);
                if (e == null || e.TrainerId != tid) return null;
                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get message by ID: {id}");
                throw;
            }
        }

        public async Task<ReadSelfMessageDto> CreateAsync(CreateSelfMessageDto dto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create trainer message");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, UpdateSelfMessageDto dto)
        {
            try
            {
                var tid = await GetTrainerId();
                var e = await _context.Messages.FindAsync(id);
                if (e == null || e.TrainerId != tid || e.SentByMember) return false;

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update message with ID: {id}");
                throw;
            }
        }
    }
}
