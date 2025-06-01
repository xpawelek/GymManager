using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberSelfService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberSelfService(
            GymDbContext context,
            MemberSelfMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private async Task<int> GetCurrentMemberId()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr); 

            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId.ToString());

            if (member == null)
                throw new Exception("Member not found");

            return member.Id;
        }

        public async Task<ReadSelfMemberDto> GetOwnAsync()
        {
            var memberId = await GetCurrentMemberId();
            var entity = await _context.Members
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == memberId);
            return _mapper.ToReadDto(entity!);
        }

        public async Task<bool> UpdateOwnAsync(UpdateSelfMemberDto dto)
        {
            var memberId = await GetCurrentMemberId();
            if (memberId == null) return false;

            var entity = await _context.Members.FindAsync(memberId);
            if (entity == null) return false;
            

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
