using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberSelfMembershipService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfMembershipMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberSelfMembershipService(
            GymDbContext context,
            MemberSelfMembershipMapper mapper,
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
            return int.TryParse(id, out var mid) ? mid : null;
        }

        public async Task<ReadSelfMembershipDto> CreateSelfMembership(CreateSelfMembershipDto selfMembershipDto)
        {
            var entity = _mapper.ToEntity(selfMembershipDto);
            await _context.Memberships.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }

        public async Task<ReadSelfMembershipDto> GetOwnAsync()
        {
            var memberId = GetCurrentMemberId()!.Value;
            var entity = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);
            return _mapper.ToReadDto(entity!);
        }

        public async Task<bool> UpdateOwnAsync(UpdateMembershipDto dto)
        {
            var memberId = GetCurrentMemberId();
            if (memberId == null) return false;

            var entity = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId);
            if (entity == null) return false;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}