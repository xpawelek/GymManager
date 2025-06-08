using System.Security.Claims;
using GymManager.Data;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberSelfTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly MemberSelfTrainerAssignmentMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberSelfTrainerAssignmentService(
            GymDbContext context,
            MemberSelfTrainerAssignmentMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private int GetCurrentMemberId() =>
            int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<int> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            var e = _mapper.ToEntity(dto);
            e.MemberId = GetCurrentMemberId();
            await _context.TrainerAssignments.AddAsync(e);
            await _context.SaveChangesAsync();
            return e.Id;
        }

        public async Task<bool> HasEverBeenAssignedAsync()
        {
            var memberId = GetCurrentMemberId();
            return await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == memberId);
        }
        public async Task<bool> HasActiveAssignmentAsync()
        {
            var memberId = GetCurrentMemberId();

            return await _context.TrainerAssignments
                .AnyAsync(a => a.MemberId == memberId && a.IsActive);
        }
    }
}
