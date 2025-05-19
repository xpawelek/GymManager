using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.AspNetCore.Http;

namespace GymManager.Services.Member
{
    public class MemberServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly MemberServiceRequestMapper _createMapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberServiceRequestService(
            GymDbContext context,
            MemberServiceRequestMapper createMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _createMapper = createMapper;
            _httpContext = httpContextAccessor;
        }

        private int? GetCurrentMemberId() =>
            int.TryParse(_httpContext.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

        public async Task<int> CreateAsync(CreateServiceRequestDto dto)
        {
            dto.MemberId = GetCurrentMemberId()!.Value;
            var e = _createMapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(e);
            await _context.SaveChangesAsync();
            return e.Id;
        }
    }
}
