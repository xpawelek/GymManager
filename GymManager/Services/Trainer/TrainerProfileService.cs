using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerProfileService
    {
        private readonly GymDbContext _context;
        private readonly TrainerProfileMapper _mapper;
        private readonly IHttpContextAccessor _http;

        public TrainerProfileService(
            GymDbContext context,
            TrainerProfileMapper mapper,
            IHttpContextAccessor http)
        {
            _context = context;
            _mapper = mapper;
            _http = http;
        }

        private string CurrentUserId()
            => _http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<ReadTrainerDto?> GetMyProfileAsync()
        {
            var uid = CurrentUserId();
            var e = await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == uid);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<bool> UpdateMyProfileAsync(UpdateSelfTrainerDto dto)
        {
            var uid = CurrentUserId();
            var e = await _context.Trainers.FirstOrDefaultAsync(t => t.UserId == uid);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
