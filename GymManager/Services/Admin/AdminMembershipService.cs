using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminMembershipService
    {
        private readonly GymDbContext _context;
        private readonly AdminMembershipMapper _mapper;

        public AdminMembershipService(
            GymDbContext context,
            AdminMembershipMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadMembershipDto>> GetAllAsync()
        {
            var list = await _context.Memberships
                .Include(m=>m.MembershipType)
                .Include(m => m.Member)
                .ToListAsync();
            
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadMembershipDto> GetByIdAsync(int id)
        {
            var entity = await _context.Memberships
                .Include(m=>m.MembershipType)
                .Include(m => m.Member)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null)
            {
                throw new Exception($"Membership with id {id} does not exist");
            }
            return _mapper.ToReadDto(entity!);
        }

        public async Task<ReadMembershipDto> CreateAsync(CreateMembershipDto dto)
        {
            var entity = _mapper.ToEntity(dto);
            
            var entityInDb = await _context.Memberships.FirstOrDefaultAsync(m => m.MemberId == entity.MemberId);

            if (entityInDb != null)
            {
                throw new Exception($"Membership with id {dto.MemberId} already exists");
            }

            var type = await _context.MembershipTypes.FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);
            if(type == null)
                throw new Exception("Invalid membership type");
            
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.StartDate.AddDays(type.DurationInDays);
            entity.IsActive = DateTime.Now >= entity.StartDate && DateTime.Now <= entity.EndDate;
            
            await _context.Memberships.AddAsync(entity);
            await _context.SaveChangesAsync();

            var full = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == entity.Id);
            
            return _mapper.ToReadDto(full!);
        }

        public async Task<bool> PatchAsync(int id, UpdateMembershipDto dto)
        {
            var entity = await _context.Memberships.FindAsync(id);
            if (entity == null) return false;

            if (dto.StartDate != null && dto.StartDate != entity.StartDate)
            {
                var membershipType = await _context.MembershipTypes
                    .FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);

                if (membershipType == null)
                    throw new Exception("Invalid membership type");

                dto.EndDate = dto.StartDate.Value.AddDays(membershipType.DurationInDays);
                dto.IsActive = DateTime.Now >= dto.StartDate && DateTime.Now <= dto.EndDate;
            }

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Memberships.FindAsync(id);
            if (entity == null) return false;
            _context.Memberships.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}