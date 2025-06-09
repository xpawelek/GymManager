using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin;

public class AdminMembershipTypeService
{
    private readonly GymDbContext _context;
    private readonly AdminMembershipTypeMapper _mapper;
    private readonly ILogger<AdminMembershipTypeService> _logger;

    public AdminMembershipTypeService(
        GymDbContext context,
        AdminMembershipTypeMapper mapper,
        ILogger<AdminMembershipTypeService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ReadMembershipTypeDto>> GetAllAsync()
    {
        try
        {
            var list = await _context.MembershipTypes.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving all membership types.");
            return new List<ReadMembershipTypeDto>();
        }
    }

    public async Task<ReadMembershipTypeDto?> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.MembershipTypes.FindAsync(id);
            return entity != null ? _mapper.ToReadDto(entity) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving membership type with ID {Id}.", id);
            return null;
        }
    }

    public async Task<ReadMembershipTypeDto?> CreateAsync(CreateMembershipTypeDto dto)
    {
        try
        {
            var entity = _mapper.ToEntity(dto);
            await _context.MembershipTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating a new membership type.");
            return null;
        }
    }

    public async Task<bool> PatchAsync(int id, UpdateMembershipTypeDto dto)
    {
        try
        {
            var entity = await _context.MembershipTypes.FindAsync(id);
            if (entity == null) return false;

            if (dto.PersonalTrainingsPerMonth == null)
                dto.PersonalTrainingsPerMonth = entity.PersonalTrainingsPerMonth;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating membership type with ID {Id}.", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.MembershipTypes.FindAsync(id);
            if (entity == null) return false;

            _context.MembershipTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting membership type with ID {Id}.", id);
            return false;
        }
    }
}
