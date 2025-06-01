using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainerService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerMapper _mapper;

        public AdminTrainerService(GymDbContext context, AdminTrainerMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainerDto>> GetAllAsync()
            => _mapper.ToReadDtoList(await _context.Trainers.ToListAsync());

        public async Task<ReadTrainerDto?> GetByIdAsync(int id)
        {
            var e = await _context.Trainers.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        /*
        public async Task<ReadTrainerDto> CreateAsync(CreateTrainerDto dto)
        {
            var e = _mapper.ToEntity(dto);
            await _context.Trainers.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }
        */

        public async Task<bool> UpdateAsync(int id, UpdateTrainerDto dto)
        {
            var entity = await _context.Trainers.FindAsync(id);
            if (entity == null) return false;

            if (dto.FirstName is not null) entity.FirstName = dto.FirstName;
            if (dto.LastName is not null) entity.LastName = dto.LastName;
            if (dto.PhoneNumber is not null) entity.PhoneNumber = dto.PhoneNumber;
            if (dto.Description is not null) entity.Description = dto.Description;
            if(dto.PhotoPath is not null) entity.PhotoPath = dto.PhotoPath;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Trainers.FindAsync(id);
            if (e == null) return false;
            _context.Trainers.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
