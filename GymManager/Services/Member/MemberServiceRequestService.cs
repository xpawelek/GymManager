using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using GymManager.Models.Mappers.Admin;

namespace GymManager.Services.Member;

public class MemberServiceRequestService
{
    private readonly GymDbContext _context;
    private readonly MemberServiceRequestMapper _mapper;

    public MemberServiceRequestService(
        GymDbContext context,
        MemberServiceRequestMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(CreateServiceRequestDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.ServiceRequests.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
