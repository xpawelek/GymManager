using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerAssignmentMapper _mapper;
        private readonly ILogger<AdminTrainerAssignmentService> _logger;

        public AdminTrainerAssignmentService(
            GymDbContext context,
            AdminTrainerAssignmentMapper mapper,
            ILogger<AdminTrainerAssignmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadTrainerAssignmentDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.TrainerAssignments
                    .Include(t => t.Trainer)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all trainer assignments.");
                return new List<ReadTrainerAssignmentDto>();
            }
        }

        public async Task<ReadTrainerAssignmentDto?> GetByMemberIdAsync(int id)
        {
            try
            {
                var e = await _context.TrainerAssignments
                    .Include(t => t.Trainer)
                    .FirstOrDefaultAsync(a => a.MemberId == id && a.IsActive == true);

                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving trainer assignment for member ID {Id}.", id);
                return null;
            }
        }

        public async Task<ReadTrainerAssignmentDto?> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            try
            {
                var checkEntityMembership = await _context.Memberships
                    .FirstOrDefaultAsync(m => m.MemberId == dto.MemberId);

                if (checkEntityMembership is null)
                {
                    _logger.LogWarning("Membership not found for member ID {MemberId}.", dto.MemberId);
                    return null;
                }

                var checkEntityMembershipType = await _context.MembershipTypes
                    .FirstOrDefaultAsync(mt => mt.Id == checkEntityMembership.MembershipTypeId);

                if (checkEntityMembershipType is null)
                {
                    _logger.LogWarning("Membership type not found for type ID {TypeId}.", checkEntityMembership.MembershipTypeId);
                    return null;
                }

                if (!checkEntityMembership.IsActive || !checkEntityMembershipType.IncludesPersonalTrainer)
                {
                    _logger.LogWarning("Member ID {MemberId} does not have an active membership or is not eligible for a personal trainer.", dto.MemberId);
                    return null;
                }

                var e = _mapper.ToEntity(dto);
                e.StartDate = checkEntityMembership.StartDate;
                e.EndDate = checkEntityMembership.EndDate;
                e.IsActive = e.EndDate == null || (DateTime.Now >= e.StartDate && DateTime.Now <= e.EndDate);

                await _context.TrainerAssignments.AddAsync(e);
                await _context.SaveChangesAsync();

                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating trainer assignment for member ID {MemberId}.", dto.MemberId);
                return null;
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainerAssignmentsDto dto)
        {
            try
            {
                var e = await _context.TrainerAssignments.FindAsync(id);
                if (e == null) return false;

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating trainer assignment with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var e = await _context.TrainerAssignments.FindAsync(id);
                if (e == null) return false;

                _context.TrainerAssignments.Remove(e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting trainer assignment with ID {Id}.", id);
                return false;
            }
        }
    }
}
