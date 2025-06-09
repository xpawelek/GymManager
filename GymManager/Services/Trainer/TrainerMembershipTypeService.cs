using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Trainer
{
    public class TrainerMembershipTypeService
    {
        private readonly GymDbContext _context;
        private readonly TrainerMembershipTypeMapper _mapper;
        private readonly ILogger<TrainerMembershipTypeService> _logger;

        public TrainerMembershipTypeService(
            GymDbContext context,
            TrainerMembershipTypeMapper mapper,
            ILogger<TrainerMembershipTypeService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadMembershipTypeDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.MembershipTypes
                    .Where(mt => mt.IsVisible)
                    .ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get visible membership types");
                throw;
            }
        }

        public async Task<ReadMembershipTypeDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.MembershipTypes.FindAsync(id);
                return entity == null ? null : _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get membership type with ID {id}");
                throw;
            }
        }
    }
}
