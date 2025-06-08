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

        private int GetCurrentMemberId() =>
            int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        
        public async Task<ReadSelfTrainerAssignmentDto> GetOwnAsync()
        {
            var memberId = await GetMemberId();
            var entity = await _context.TrainerAssignments
                .Include(m => m.Member)
                .Include(m => m.Trainer)
                .FirstOrDefaultAsync(a => a.MemberId == memberId && a.IsActive);

            if (entity == null)
            {
                throw new Exception("Active assignment not found");
            }
            
            return _mapper.ToReadDto(entity!);
        }

        public async Task<int> CreateAsync(CreateTrainerAssignmentDto dto)
        {
            var e = _mapper.ToEntity(dto);
            e.MemberId = await GetMemberId();
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
