using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly MemberEquipmentMapper _mapper;
        private readonly ILogger<MemberEquipmentService> _logger;

        public MemberEquipmentService(
            GymDbContext context,
            MemberEquipmentMapper mapper,
            ILogger<MemberEquipmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadEquipmentDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.Equipments.ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all equipment for member view.");
                return new List<ReadEquipmentDto>();
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
                _logger.LogError(ex, "Error while retrieving equipment with ID {Id} for member.", id);
                return null;
            }
        }
    }
}
