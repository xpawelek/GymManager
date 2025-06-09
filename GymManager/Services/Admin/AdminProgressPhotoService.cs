using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly AdminProgessPhotoMapper _mapper;
        private readonly ILogger<AdminProgressPhotoService> _logger;

        public AdminProgressPhotoService(
            GymDbContext context,
            AdminProgessPhotoMapper mapper,
            ILogger<AdminProgressPhotoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.ProgressPhotos.ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all progress photos.");
                return new List<ReadProgressPhotoDto>();
            }
        }

        public async Task<ReadProgressPhotoDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.ProgressPhotos.FindAsync(id);
                return entity != null ? _mapper.ToReadDto(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving progress photo with ID {Id}.", id);
                return null;
            }
        }

        public async Task<List<ReadProgressPhotoDto>> GetAllPublic()
        {
            try
            {
                var list = await _context.ProgressPhotos
                    .Where(p => p.IsPublic == true)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving public progress photos.");
                return new List<ReadProgressPhotoDto>();
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateProgressPhotoDto dto)
        {
            try
            {
                var entity = await _context.ProgressPhotos.FindAsync(id);
                if (entity == null) return false;

                if (dto.Comment == null)
                    dto.Comment = entity.Comment;

                _mapper.UpdateEntity(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating progress photo with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.ProgressPhotos.FindAsync(id);
                if (entity == null) return false;

                _context.ProgressPhotos.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting progress photo with ID {Id}.", id);
                return false;
            }
        }
    }
}
