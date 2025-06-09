using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin;

public class AdminMessageService
{
    private readonly GymDbContext _context;
    private readonly AdminMessageMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;
    private readonly ILogger<AdminMessageService> _logger;

    public AdminMessageService(
        GymDbContext context,
        AdminMessageMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AdminMessageService> logger)
    {
        _context = context;
        _mapper = mapper;
        _httpContext = httpContextAccessor;
        _logger = logger;
    }

    public async Task<List<ReadMessageDto>> GetAllAsync()
    {
        try
        {
            var list = await _context.Messages.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving all messages.");
            return new List<ReadMessageDto>();
        }
    }

    public async Task<ReadMessageDto?> GetByIdAsync(int id)
    {
        try
        {
            var e = await _context.Messages.FindAsync(id);
            return e == null ? null : _mapper.ToReadDto(e);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving message with ID {Id}.", id);
            return null;
        }
    }
}
