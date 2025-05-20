using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;

namespace GymManager.Services.Trainer
{
    public class TrainerServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly TrainerServiceRequestMapper _createMapper;
        private readonly TrainerServiceRequestMapper _readMapper;

        public TrainerServiceRequestService(
            GymDbContext context,
            TrainerServiceRequestMapper createMapper,
            TrainerServiceRequestMapper readMapper)
        {
            _context = context;
            _createMapper = createMapper;
            _readMapper = readMapper;
        }

        public async Task<ReadServiceRequestDto> CreateAsync(CreateServiceRequestDto dto)
        {
            var e = _createMapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(e);
            await _context.SaveChangesAsync();
            return _readMapper.ToReadDto(e);
        }
    }
}
