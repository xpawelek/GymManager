using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly TrainerServiceRequestMapper _mapper;
        private readonly ILogger<TrainerServiceRequestService> _logger;

        public TrainerServiceRequestService(
            GymDbContext context,
            TrainerServiceRequestMapper mapper,
            ILogger<TrainerServiceRequestService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(CreateServiceRequestDto dto)
        {
            try
            {
                var entity = _mapper.ToEntity(dto);
                await _context.ServiceRequests.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create service request");
                return false;
            }
        }
    }
}
