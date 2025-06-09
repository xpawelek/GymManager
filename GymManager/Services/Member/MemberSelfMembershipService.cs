using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberSelfMembershipService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMembershipMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberSelfMembershipService> _logger;

        public MemberSelfMembershipService(
            GymDbContext context,
            MemberSelfMembershipMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberSelfMembershipService> logger)
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

        public async Task<int> GetMemberIdAsync(string userId)
        {
            try
            {
                var member = await _context.Members
                    .FirstOrDefaultAsync(m => m.UserId == userId);

                if (member == null)
                    throw new Exception("Member not found.");

                return member.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving member ID for user ID {UserId}.", userId);
                throw;
            }
        }

        public async Task<bool> HasOrHadAnyMembershipAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                return await _context.Memberships
                    .AnyAsync(m => m.MemberId == memberId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking membership existence.");
                return false;
            }
        }

        public async Task<ReadSelfMembershipDto?> CreateSelfMembership(CreateSelfMembershipDto selfMembershipDto)
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = _mapper.ToEntity(selfMembershipDto);
                entity.MemberId = memberId;

                var entityInDb = await _context.Memberships
                    .FirstOrDefaultAsync(m => m.MemberId == entity.MemberId && m.IsActive);

                if (entityInDb != null)
                {
                    _logger.LogWarning("Member ID {MemberId} already has an active membership.", memberId);
                    return null;
                }

                var type = await _context.MembershipTypes
                    .FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);

                if (type == null)
                {
                    _logger.LogWarning("Invalid membership type ID {TypeId}.", entity.MembershipTypeId);
                    return null;
                }

                entity.StartDate = DateTime.Now;
                entity.EndDate = DateTime.Now.AddDays(type.DurationInDays);
                entity.IsActive = DateTime.Now >= entity.StartDate && DateTime.Now <= entity.EndDate;

                await _context.Memberships.AddAsync(entity);
                await _context.SaveChangesAsync();

                var full = await _context.Memberships
                    .Include(m => m.Member)
                    .Include(m => m.MembershipType)
                    .FirstOrDefaultAsync(m => m.Id == entity.Id);

                return full != null ? _mapper.ToReadDto(full) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating self-membership.");
                return null;
            }
        }

        public async Task<ReadSelfMembershipDto?> GetOwnAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.Memberships
                    .Include(m => m.Member)
                    .Include(m => m.MembershipType)
                    .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving active membership for current member.");
                return null;
            }
        }

        public async Task<bool> UpdateOwnAsync(UpdateMembershipDto dto)
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.Memberships
                    .FirstOrDefaultAsync(m => m.MemberId == memberId);

                if (entity == null) return false;

                if (dto.StartDate != null && dto.StartDate != entity.StartDate)
                {
                    var membershipType = await _context.MembershipTypes
                        .FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);

                    if (membershipType == null)
                    {
                        _logger.LogWarning("Invalid membership type ID {TypeId} for update.", entity.MembershipTypeId);
                        return false;
                    }

                    dto.EndDate = dto.StartDate.Value.AddDays(membershipType.DurationInDays);
                    dto.IsActive = DateTime.Now >= dto.StartDate && DateTime.Now <= dto.EndDate;
                }

                _mapper.UpdateEntity(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating own membership.");
                return false;
            }
        }
    }
}
