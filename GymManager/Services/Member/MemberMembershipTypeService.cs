using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberMembershipTypeService
    {
        private readonly GymDbContext _context;
        private readonly MemberMembershipTypeMapper _mapper;
        private readonly ILogger<MemberMembershipTypeService> _logger;

        public MemberMembershipTypeService(
            GymDbContext context,
            MemberMembershipTypeMapper mapper,
            ILogger<MemberMembershipTypeService> logger)
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
                _logger.LogError(ex, "Error while retrieving visible membership types for member.");
                return new List<ReadMembershipTypeDto>();
            }
        }

        public async Task<ReadMembershipTypeDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.MembershipTypes.FindAsync(id);
                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving membership type with ID {Id}.", id);
                return null;
            }
        }
    }
}
