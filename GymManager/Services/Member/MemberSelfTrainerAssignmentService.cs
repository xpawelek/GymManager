using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberSelfTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfTrainerAssignmentMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberSelfTrainerAssignmentService> _logger;

        public MemberSelfTrainerAssignmentService(
            GymDbContext context,
            MemberSelfTrainerAssignmentMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberSelfTrainerAssignmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _logger = logger;
        }

        private async Task<int> GetMemberIdAsync()
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

        public async Task<ReadSelfTrainerAssignmentDto?> GetOwnAsync()
        {
            try
            {
                var memberId = await GetMemberIdAsync();
                var entity = await _context.TrainerAssignments
                    .Include(m => m.Member)
                    .Include(m => m.Trainer)
                    .FirstOrDefaultAsync(a => a.MemberId == memberId && a.IsActive);

                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving current trainer assignment.");
                return null;
            }
        }

        public async Task<int?> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            try
            {
                var e = _mapper.ToEntity(dto);
                e.MemberId = await GetMemberIdAsync();
                e.IsActive = true;

                await _context.TrainerAssignments.AddAsync(e);
                await _context.SaveChangesAsync();

                return e.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating trainer assignment.");
                return null;
            }
        }

        public async Task<bool> HasEverBeenAssignedAsync()
        {
            try
            {
                var memberId = await GetMemberIdAsync();
                return await _context.TrainerAssignments
                    .AnyAsync(a => a.MemberId == memberId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if member has ever been assigned.");
                return false;
            }
        }

        public async Task<bool> HasActiveAssignmentAsync()
        {
            try
            {
                var memberId = await GetMemberIdAsync();
                return await _context.TrainerAssignments
                    .AnyAsync(a => a.MemberId == memberId && a.IsActive);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking if member has an active trainer assignment.");
                return false;
            }
        }
    }
}
