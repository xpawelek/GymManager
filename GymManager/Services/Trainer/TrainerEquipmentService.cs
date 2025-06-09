using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly TrainerEquipmentMapper _mapper;
        private readonly ILogger<TrainerEquipmentService> _logger;

        public TrainerEquipmentService(GymDbContext context, TrainerEquipmentMapper mapper, ILogger<TrainerEquipmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadEquipmentDto>> GetAllAsync()
        {
            try
            {
                var equipments = await _context.Equipments.ToListAsync();
                return _mapper.ToReadDtoList(equipments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve equipment list");
                throw;
            }
        }

        public async Task<ReadEquipmentDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.Equipments.FindAsync(id);
                return e == null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to retrieve equipment with ID: {id}");
                throw;
            }
        }
    }
}
