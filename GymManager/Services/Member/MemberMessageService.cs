using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberMessageService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMessageMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberMessageService> _logger;

        public MemberMessageService(
            GymDbContext context,
            MemberSelfMessageMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberMessageService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetMemberId()
        {
            try
            {
                var userIdStr = _httpContext.HttpContext?
                    .User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdStr))
                    throw new Exception("User ID not found in token.");

                var userId = Guid.Parse(userIdStr);

                var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.UserId == userId.ToString());

                if (member == null)
                    throw new Exception("Member not found.");

                return member.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving current member ID.");
                throw;
            }
        }

        public async Task<List<ReadSelfMessageDto>> GetAllAsync()
        {
            try
            {
                var mid = await GetMemberId();
                var list = await _context.Messages
                    .Where(m => m.MemberId == mid)
                    .OrderByDescending(m => m.Date)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all messages for current member.");
                return new List<ReadSelfMessageDto>();
            }
        }

        public async Task<ReadSelfMessageDto?> GetByIdAsync(int id)
        {
            try
            {
                var mid = await GetMemberId();
                var e = await _context.Messages.FindAsync(id);

                if (e == null || e.MemberId != mid)
                    return null;

                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving message with ID {Id}.", id);
                return null;
            }
        }

        public async Task<ReadSelfMessageDto?> CreateAsync(CreateMessageDto dto)
        {
            try
            {
                var mid = await GetMemberId();

                var assignment = await _context.TrainerAssignments
                    .AnyAsync(a => a.MemberId == mid && a.TrainerId == dto.TrainerId && a.IsActive);

                if (!assignment)
                {
                    _logger.LogWarning("Member ID {MemberId} is not assigned to trainer ID {TrainerId}.", mid, dto.TrainerId);
                    return null;
                }

                var e = _mapper.ToEntity(dto);
                e.MemberId = mid;
                e.SentByMember = true;

                await _context.Messages.AddAsync(e);
                await _context.SaveChangesAsync();

                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating message from member.");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(int id, UpdateMessageDto dto)
        {
            try
            {
                var mid = await GetMemberId();
                var e = await _context.Messages.FindAsync(id);

                if (e == null || e.MemberId != mid || !e.SentByMember)
                {
                    _logger.LogWarning("Unauthorized update attempt for message ID {Id} by member ID {MemberId}.", id, mid);
                    return false;
                }

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating message with ID {Id}.", id);
                return false;
            }
        }
    }
}
