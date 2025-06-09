using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly MemberProgressPhotoMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberProgressPhotoService> _logger;

        public MemberProgressPhotoService(
            GymDbContext context,
            MemberProgressPhotoMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<MemberProgressPhotoService> logger)
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

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var list = await _context.ProgressPhotos
                    .Where(p => p.MemberId == memberId)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving progress photos for current member.");
                return new List<ReadProgressPhotoDto>();
            }
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllPublic()
        {
            try
            {
                var list = await _context.ProgressPhotos
                    .Where(p => p.IsPublic == true)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving public progress photos.");
                return new List<ReadProgressPhotoDto>();
            }
        }

        public async Task<ReadProgressPhotoDto?> GetByIdAsync(int id)
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.ProgressPhotos
                    .FirstOrDefaultAsync(p => p.Id == id && p.MemberId == memberId);

                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving progress photo with ID {Id}.", id);
                return null;
            }
        }

        public async Task<ReadProgressPhotoDto?> CreateAsync(CreateProgressPhotoDto dto)
        {
            try
            {
                dto.MemberId = await GetCurrentMemberId();
                dto.Date = DateTime.Now;

                var entity = _mapper.ToEntity(dto);
                await _context.ProgressPhotos.AddAsync(entity);
                await _context.SaveChangesAsync();

                return _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating progress photo.");
                return null;
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateProgressPhotoDto dto)
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.ProgressPhotos.FindAsync(id);

                if (entity == null || entity.MemberId != memberId)
                    return false;

                _mapper.UpdateEntity(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating progress photo with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var memberId = await GetCurrentMemberId();
                var entity = await _context.ProgressPhotos.FindAsync(id);

                if (entity == null || entity.MemberId != memberId)
                    return false;

                _context.ProgressPhotos.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting progress photo with ID {Id}.", id);
                return false;
            }
        }
    }
}
