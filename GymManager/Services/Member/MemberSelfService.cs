using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberSelfService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberSelfService> _logger;

        public MemberSelfService(
            GymDbContext context,
            MemberSelfMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberSelfService> logger)
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

        public async Task<ReadSelfMemberDto?> GetOwnAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.Members
                    .Include(m => m.User)
                    .FirstOrDefaultAsync(m => m.Id == memberId);

                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving member self-profile.");
                return null;
            }
        }

        public async Task<bool> UpdateOwnAsync(UpdateSelfMemberDto dto)
        {
            try
            {
                var memberId = await GetCurrentMemberId();

                var entity = await _context.Members.FindAsync(memberId);
                if (entity == null) return false;

                _mapper.UpdateEntity(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating member self-profile.");
                return false;
            }
        }
    }
}
