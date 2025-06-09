using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminMembershipService
    {
        private readonly GymDbContext _context;
        private readonly AdminMembershipMapper _mapper;
        private readonly ILogger<AdminMembershipService> _logger;

        public AdminMembershipService(
            GymDbContext context,
            AdminMembershipMapper mapper,
            ILogger<AdminMembershipService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadMembershipDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.Memberships
                    .Include(m => m.MembershipType)
                    .Include(m => m.Member)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all memberships.");
                return new List<ReadMembershipDto>();
            }
        }

        public async Task<ReadMembershipDto?> GetByMemberIdAsync(int memberId)
        {
            try
            {
                var entity = await _context.Memberships
                    .Include(m => m.MembershipType)
                    .Include(m => m.Member)
                    .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

                if (entity == null)
                {
                    _logger.LogWarning("Active membership not found for member with ID {MemberId}.", memberId);
                    return null;
                }

                return _mapper.ToReadDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving membership for member ID {MemberId}.", memberId);
                return null;
            }
        }

        public async Task<ReadMembershipDto?> CreateAsync(CreateMembershipDto dto)
        {
            try
            {
                var entity = _mapper.ToEntity(dto);

                var entityInDb = await _context.Memberships.FirstOrDefaultAsync(m => m.MemberId == entity.MemberId);
                if (entityInDb != null)
                {
                    _logger.LogWarning("Membership already exists for member ID {MemberId}.", dto.MemberId);
                    return null;
                }

                var type = await _context.MembershipTypes.FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);
                if (type == null)
                {
                    _logger.LogWarning("Invalid membership type ID: {TypeId}", entity.MembershipTypeId);
                    return null;
                }

                entity.StartDate = dto.StartDate;
                entity.EndDate = dto.StartDate.AddDays(type.DurationInDays);
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
                _logger.LogError(ex, "Error while creating membership for member ID {MemberId}.", dto.MemberId);
                return null;
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateMembershipDto dto)
        {
            try
            {
                var entity = await _context.Memberships.FindAsync(id);
                if (entity == null) return false;

                if (dto.StartDate != null && dto.StartDate != entity.StartDate)
                {
                    var membershipType = await _context.MembershipTypes
                        .FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);

                    if (membershipType == null)
                    {
                        _logger.LogWarning("Invalid membership type for membership ID {Id}.", id);
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
                _logger.LogError(ex, "Error while updating membership with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Memberships.FindAsync(id);
                if (entity == null) return false;

                _context.Memberships.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting membership with ID {Id}.", id);
                return false;
            }
        }
    }
}
