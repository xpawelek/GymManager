using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin;

public class AdminServiceRequestService
{
    private readonly GymDbContext _context;
    private readonly AdminServiceRequestMapper _mapper;
    private readonly ILogger<AdminServiceRequestService> _logger;

    public AdminServiceRequestService(
        GymDbContext context,
        AdminServiceRequestMapper mapper,
        ILogger<AdminServiceRequestService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<ReadServiceRequestDto>> GetAllAsync()
    {
        try
        {
            var list = await _context.ServiceRequests.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving all service requests.");
            return new List<ReadServiceRequestDto>();
        }
    }

    public async Task<ReadServiceRequestDto?> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.ServiceRequests.FindAsync(id);
            return entity is null ? null : _mapper.ToReadDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving service request with ID {Id}.", id);
            return null;
        }
    }

    public async Task<ReadServiceRequestDto?> CreateAsync(CreateServiceRequestDto dto)
    {
        try
        {
            var entity = _mapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating a new service request.");
            return null;
        }
    }

    public async Task<bool> PatchAsync(int id, UpdateServiceRequestDto dto)
    {
        try
        {
            var entity = await _context.ServiceRequests.FindAsync(id);
            if (entity is null)
                return false;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating service request with ID {Id}.", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.ServiceRequests.FindAsync(id);
            if (entity is null)
                return false;

            _context.ServiceRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting service request with ID {Id}.", id);
            return false;
        }
    }

    public async Task<bool> ToggleResolvedAsync(int id)
    {
        try
        {
            var entity = await _context.ServiceRequests.FindAsync(id);
            if (entity is null)
                return false;

            entity.IsResolved = !entity.IsResolved;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while toggling IsResolved on service request with ID {Id}.", id);
            return false;
        }
    }
}
