using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.DTOs.Member;
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
            var list = await _context.Memberships.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadMembershipDto> GetByIdAsync(int id)
        {
            var entity = await _context.Memberships.FindAsync(id);
            return _mapper.ToReadDto(entity!);
        }

        public async Task<ReadMembershipDto> CreateAsync(CreateMembershipDto dto)
        {
            var entity = _mapper.ToEntity(dto);
            await _context.Memberships.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }

        public async Task<bool> PatchAsync(int id, UpdateMembershipDto dto)
        {
            var entity = await _context.Memberships.FindAsync(id);
            if (entity == null) return false;
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