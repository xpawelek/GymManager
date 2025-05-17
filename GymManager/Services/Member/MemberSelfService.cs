using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;

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

        private int? GetCurrentMemberId()
        {
            var id = _httpContext.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(id, out var memberId) ? memberId : null;
        }

        public async Task<ReadSelfMemberDto> GetOwnAsync()
        {
            var memberId = GetCurrentMemberId()!;
            var entity = await _context.Members.FindAsync(memberId);
            return _mapper.ToReadDto(entity!);
        }

        public async Task<bool> UpdateOwnAsync(UpdateSelfMemberDto dto)
        {
            var memberId = GetCurrentMemberId();
            if (memberId == null) return false;

            var entity = await _context.Members.FindAsync(memberId.Value);
            if (entity == null) return false;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
