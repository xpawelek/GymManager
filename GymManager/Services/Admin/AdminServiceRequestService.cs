using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin;

public class AdminServiceRequestService
{
    private readonly GymDbContext _context;
    private readonly AdminServiceRequestMapper _mapper;

    public AdminServiceRequestService(GymDbContext context, AdminServiceRequestMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReadServiceRequestDto>> GetAllAsync()
    {
        var list = await _context.ServiceRequests.ToListAsync();
        return _mapper.ToReadDtoList(list);
    }

    public async Task<ReadServiceRequestDto?> GetByIdAsync(int id)
    {
        var entity = await _context.ServiceRequests.FindAsync(id);
        return entity is null ? null : _mapper.ToReadDto(entity);
    }

    public async Task<ReadServiceRequestDto> CreateAsync(CreateServiceRequestDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.ServiceRequests.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.ToReadDto(entity);
    }

    public async Task<bool> PatchAsync(int id, UpdateServiceRequestDto dto)
    {
        var entity = await _context.ServiceRequests.FindAsync(id);
        if (entity is null)
            return false;

        _mapper.UpdateEntity(dto, entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ServiceRequests.FindAsync(id);
        if (entity is null)
            return false;

        _context.ServiceRequests.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleResolvedAsync(int id)
    {
        var entity = await _context.ServiceRequests.FindAsync(id);
        if (entity is null)
            return false;

        entity.IsResolved = !entity.IsResolved;
        await _context.SaveChangesAsync();
        return true;
    }
}
