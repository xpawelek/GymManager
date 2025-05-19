using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.AspNetCore.Http;

namespace GymManager.Services.Trainer
{
    public class TrainerServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly TrainerServiceRequestMapper _createMapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerServiceRequestService(
            GymDbContext context,
            TrainerServiceRequestMapper createMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _createMapper = createMapper;
            _httpContext = httpContextAccessor;
        }

        private int? GetCurrentTrainerId() =>
            int.TryParse(_httpContext.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        public async Task<int> CreateAsync(CreateServiceRequestDto dto)
        {
            dto.TrainerId = GetCurrentTrainerId()!.Value;
            var e = _createMapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(e);
            await _context.SaveChangesAsync();
            return e.Id;
        }
    }
    }
}
