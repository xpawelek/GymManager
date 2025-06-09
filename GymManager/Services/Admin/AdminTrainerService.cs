using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminTrainerService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainerMapper _mapper;
        private readonly ILogger<AdminTrainerService> _logger;

        public AdminTrainerService(GymDbContext context, AdminTrainerMapper mapper, ILogger<AdminTrainerService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadTrainerDto>> GetAllAsync()
        {
            try
            {
                var trainers = await _context.Trainers.ToListAsync();
                return _mapper.ToReadDtoList(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all trainers.");
                return new List<ReadTrainerDto>();
            }
        }

        public async Task<ReadTrainerDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.Trainers.FindAsync(id);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving trainer with ID {Id}.", id);
                return null;
            }
        }

        /*
        public async Task<ReadTrainerDto?> CreateAsync(CreateTrainerDto dto)
        {
            try
            {
                var e = _mapper.ToEntity(dto);
                await _context.Trainers.AddAsync(e);
                await _context.SaveChangesAsync();
                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a new trainer.");
                return null;
            }
        }
        */

        public async Task<bool> UpdateAsync(int id, UpdateTrainerDto dto)
        {
            try
            {
                var entity = await _context.Trainers.FindAsync(id);
                if (entity == null) return false;

                if (dto.FirstName is not null) entity.FirstName = dto.FirstName;
                if (dto.LastName is not null) entity.LastName = dto.LastName;
                if (dto.PhoneNumber is not null) entity.PhoneNumber = dto.PhoneNumber;
                if (dto.Description is not null) entity.Description = dto.Description;
                if (dto.PhotoPath is not null) entity.PhotoPath = dto.PhotoPath;

                _mapper.UpdateEntity(dto, entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating trainer with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var e = await _context.Trainers.FindAsync(id);
                if (e == null) return false;

                _context.Trainers.Remove(e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting trainer with ID {Id}.", id);
                return false;
            }
        }
    }
}
