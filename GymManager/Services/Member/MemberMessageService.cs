using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
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

        private async Task<int> GetMemberId()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr); 

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.UserId == userId.ToString());

            if (member == null)
                throw new Exception("Member not found");

            return member.Id;
        }

        public async Task<List<ReadSelfMessageDto>> GetAllAsync()
        {
            var mid = await GetMemberId();
            var list = await _context.Messages
                .Where(m => m.MemberId == mid)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfMessageDto?> GetByIdAsync(int id)
        {
            var mid = await GetMemberId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.MemberId != mid) return null;
            return _mapper.ToReadDto(e);
        }

        public async Task<ReadSelfMessageDto> CreateAsync(CreateMessageDto dto)
        {
            var mid = await GetMemberId();
            var assignment = await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == mid && a.TrainerId == dto.TrainerId && a.IsActive == true);
            if (!assignment)
                throw new InvalidOperationException("You are not connected to this trainer");

            var e = _mapper.ToEntity(dto);
            e.MemberId = mid;
            e.SentByMember = true;
            await _context.Messages.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> UpdateAsync(int id, UpdateMessageDto dto)
        {
            var mid = await GetMemberId();
            var e = await _context.Messages.FindAsync(id);
            if (e == null || e.MemberId != mid || !e.SentByMember)
            {
                throw new Exception("Cannot update message!");
            }
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
