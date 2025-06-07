using GymManager.Data;
using GymManager.Shared.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;

namespace GymManager.Services.Trainer;

public class TrainerServiceRequestService
{
    private readonly GymDbContext _context;
    private readonly TrainerServiceRequestMapper _mapper;

    public TrainerServiceRequestService(
        GymDbContext context,
        TrainerServiceRequestMapper mapper)
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
