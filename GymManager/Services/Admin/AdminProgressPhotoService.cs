using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly AdminProgessPhotoMapper _mapper;

        public AdminProgressPhotoService(
            GymDbContext context,
            AdminProgessPhotoMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            var list = await _context.ProgressPhotos.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadProgressPhotoDto> GetByIdAsync(int id)
        {
            var entity = await _context.ProgressPhotos.FindAsync(id);
            return _mapper.ToReadDto(entity!);
        }

        public async Task<bool> PatchAsync(int id, UpdateProgressPhotoDto dto)
        {
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null) return false;
            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null) return false;
            _context.ProgressPhotos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
