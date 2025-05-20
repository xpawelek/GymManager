using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberMessageService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMessageMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberMessageService(
            GymDbContext context,
            MemberSelfMessageMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private int GetMemberId() =>
            int.Parse(_httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<List<ReadSelfMessageDto>> GetAllAsync()
        {
            var mid = GetMemberId();
            var list = await _context.Messages
                .Where(m => m.MemberId == mid)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfMessageDto?> GetByIdAsync(int id)
        {
            var mid = GetMemberId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.MemberId != mid) return null;
            return _mapper.ToReadDto(e);
        }

        public async Task<ReadSelfMessageDto> CreateAsync(CreateMessageDto dto)
        {
            var mid = GetMemberId();
            var assignment = await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == mid && a.TrainerId == dto.TrainerId && a.IsActive);
            if (!assignment)
                throw new InvalidOperationException("You are not connected to this trainer");

            var e = _mapper.ToEntity(dto);
            e.MemberId = mid;
            await _context.Messages.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> UpdateAsync(int id, UpdateMessageDto dto)
        {
            var mid = GetMemberId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.MemberId != mid) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
