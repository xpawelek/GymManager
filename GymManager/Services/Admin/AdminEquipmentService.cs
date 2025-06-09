using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminEquipmentService
    {
        private readonly GymDbContext _context;
        private readonly AdminEquipmentMapper _mapper;
        private readonly ILogger<AdminEquipmentService> _logger;

        public AdminEquipmentService(GymDbContext context, AdminEquipmentMapper mapper, ILogger<AdminEquipmentService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadEquipmentDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.Equipments.ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all equipment.");
                return new List<ReadEquipmentDto>();
            }
        }

        public async Task<ReadEquipmentDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.Equipments.FindAsync(id);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving equipment with ID {Id}.", id);
                return null;
            }
        }

        public async Task<ReadEquipmentDto?> CreateAsync(CreateEquipmentDto dto)
        {
            try
            {
                var e = _mapper.ToEntity(dto);
                await _context.Equipments.AddAsync(e);
                await _context.SaveChangesAsync();
                return _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new equipment.");
                return null;
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateEquipmentDto dto)
        {
            try
            {
                var e = await _context.Equipments.FindAsync(id);
                if (e == null) return false;

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating equipment with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var e = await _context.Equipments.FindAsync(id);
                if (e == null) return false;

                _context.Equipments.Remove(e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting equipment with ID {Id}.", id);
                return false;
            }
        }
    }
}
