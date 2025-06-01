using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin;

public class AdminMessageService
{
    private readonly GymDbContext _context;
    private readonly AdminMessageMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public AdminMessageService(
        GymDbContext context,
        AdminMessageMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContext = httpContextAccessor;
    }
    
    public async Task<List<ReadMessageDto>> GetAllAsync()
    {
        var list = await _context.Messages
            .ToListAsync();
        return _mapper.ToReadDtoList(list);
    }

    public async Task<ReadMessageDto?> GetByIdAsync(int id)
    {
        var e = await _context.Messages.FindAsync(id);
        if (e == null) return null;
        return _mapper.ToReadDto(e);
    }
}