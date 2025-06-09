using System.Security.Claims;
using GymManager.Data;
using GymManager.Exceptions;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerProfileService
    {
        private readonly GymDbContext _context;
        private readonly TrainerProfileMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly ILogger<TrainerProfileService> _logger;

        public TrainerProfileService(
            GymDbContext context,
            TrainerProfileMapper mapper,
            IHttpContextAccessor http,
            ILogger<TrainerProfileService> logger)
        {
            _context = context;
            _mapper = mapper;
            _http = http;
            _logger = logger;
        }

        private string CurrentUserId()
        {
            var userId = _http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("Trainer user ID not found in token");
                throw new UserFacingException("Unable to determine current user");
            }
            return userId;
        }

        public async Task<ReadTrainerDto?> GetMyProfileAsync()
        {
            try
            {
                var uid = CurrentUserId();
                var e = await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == uid);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get trainer profile");
                throw;
            }
        }

        public async Task<bool> UpdateMyProfileAsync(UpdateSelfTrainerDto dto)
        {
            try
            {
                var uid = CurrentUserId();
                var e = await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == uid);
                if (e == null)
                {
                    _logger.LogWarning("Trainer not found for user ID: {UserId}", uid);
                    return false;
                }

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update trainer profile");
                throw;
            }
        }
    }
}
