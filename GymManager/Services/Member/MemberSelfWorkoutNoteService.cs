using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberSelfWorkoutNoteService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfWorkoutNoteMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberSelfWorkoutNoteService> _logger;

        public MemberSelfWorkoutNoteService(
            GymDbContext context,
            MemberSelfWorkoutNoteMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberSelfWorkoutNoteService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetCurrentMemberId()
        {
            try
            {
                var userIdStr = _httpContext.HttpContext?
                    .User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdStr))
                    throw new Exception("User ID not found in token.");

                var userId = Guid.Parse(userIdStr);

                var member = await _context.Members
                    .FirstOrDefaultAsync(t => t.UserId == userId.ToString());

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

        public async Task<List<ReadSelfWorkoutNoteDto>> GetAllAsync()
        {
            try
            {
                var mid = await GetCurrentMemberId();
                var list = await _context.WorkoutNotes
                    .Where(n => n.MemberId == mid)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving workout notes for member.");
                return new List<ReadSelfWorkoutNoteDto>();
            }
        }

        public async Task<ReadSelfWorkoutNoteDto?> GetByIdAsync(int id)
        {
            try
            {
                var mid = await GetCurrentMemberId();
                var e = await _context.WorkoutNotes
                    .FirstOrDefaultAsync(w => w.TrainingSessionId == id && w.MemberId == mid);

                return e != null ? _mapper.ToReadDto(e) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving workout note with training session ID {Id}.", id);
                return null;
            }
        }
    }
}
