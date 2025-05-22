using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerAssignmentMapper _mapper;

        public AdminTrainerAssignmentService(
            GymDbContext context,
            AdminTrainerAssignmentMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainerAssignmentDto>> GetAllAsync()
        {
            var list = await _context.TrainerAssignments.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainerAssignmentDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<ReadTrainerAssignmentDto> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            var checkEntityMembership = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == dto.MemberId);

            if (checkEntityMembership is null)
                throw new Exception("Membership not found for given member.");

            var checkEntityMembershipType = await _context.MembershipTypes
                .FirstOrDefaultAsync(mt => mt.Id == checkEntityMembership.MembershipTypeId);

            if (checkEntityMembershipType is null)
                throw new Exception("Membership type not found.");

            if (checkEntityMembership is null || checkEntityMembershipType is null)
            {
                throw new NullReferenceException();
            }
            
            if (checkEntityMembership.IsActive == false || checkEntityMembershipType.IncludesPersonalTrainer == false || checkEntityMembershipType.IncludesPersonalTrainer == false)
            {
                throw new Exception("Member doesn't have active membership or membership type doesn't allow for having trainer.");
            }
            
            var e = _mapper.ToEntity(dto);
            
            e.StartDate = checkEntityMembership.StartDate;
            e.EndDate = checkEntityMembership.EndDate;
            e.IsActive = e.EndDate == null ||
                         (DateTime.Now >= e.StartDate && DateTime.Now <= e.EndDate);
            
            await _context.TrainerAssignments.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainerAssignmentsDto dto)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.TrainerAssignments.FindAsync(id);
            if (e == null) return false;
            _context.TrainerAssignments.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
