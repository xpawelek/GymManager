using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using GymManager.Models.Mappers.Admin;

namespace GymManager.Services.Member
{
    public class MemberServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly MemberServiceRequestMapper _createMapper;
        private readonly AdminServiceRequestMapper _readMapper;

        public MemberServiceRequestService(
            GymDbContext context,
            MemberServiceRequestMapper createMapper,
            AdminServiceRequestMapper readMapper)
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
