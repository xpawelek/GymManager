using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member;

public class MemberServiceRequestService
{
    private readonly GymDbContext _context;
    private readonly MemberServiceRequestMapper _mapper;
    private readonly ILogger<MemberServiceRequestService> _logger;

    public MemberServiceRequestService(
        GymDbContext context,
        MemberServiceRequestMapper mapper,
        ILogger<MemberServiceRequestService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> CreateAsync(CreateServiceRequestDto dto)
    {
        try
        {
            var entity = _mapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating service request.");
            return false;
        }
    }
}
