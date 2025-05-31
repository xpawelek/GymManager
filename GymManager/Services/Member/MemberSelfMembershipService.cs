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

        private async Task<int> GetCurrentMemberId()
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

        public async Task<ReadSelfMembershipDto> CreateSelfMembership(CreateSelfMembershipDto selfMembershipDto)
        {
            var entity = _mapper.ToEntity(selfMembershipDto);
            
            var entityInDb = await _context.Memberships.FirstOrDefaultAsync(m => m.MemberId == entity.MemberId);

            if (entityInDb != null)
            {
                throw new Exception($"Membership already exists");
            }

            var type = await _context.MembershipTypes.FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);
            if(type == null)
                throw new Exception("Invalid membership type");
            
            entity.StartDate = DateTime.Now;
            entity.EndDate = DateTime.Now.AddDays(type.DurationInDays);
            entity.IsActive = DateTime.Now >= entity.StartDate && DateTime.Now <= entity.EndDate;
            
            await _context.Memberships.AddAsync(entity);
            await _context.SaveChangesAsync();

            var full = await _context.Memberships
                .Include(m => m.Member)
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == entity.Id);
            
            return _mapper.ToReadDto(full!);
        }

        public async Task<ReadSelfMembershipDto> GetOwnAsync()
        {
            var memberId = await GetCurrentMemberId();
            var entity = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

            if (entity == null)
            {
                throw new Exception("Active membership not found");
            }
            
            return _mapper.ToReadDto(entity!);
        }

        public async Task<bool> UpdateOwnAsync(UpdateMembershipDto dto)
        {
            var memberId = await GetCurrentMemberId();
            var entity = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId);
            if (entity == null) return false;

            if (dto.StartDate != null && dto.StartDate != entity.StartDate)
            {
                var membershipType = await _context.MembershipTypes
                    .FirstOrDefaultAsync(t => t.Id == entity.MembershipTypeId);

                if (membershipType == null)
                    throw new Exception("Invalid membership type");

                dto.EndDate = dto.StartDate.Value.AddDays(membershipType.DurationInDays);
                dto.IsActive = DateTime.Now >= dto.StartDate && DateTime.Now <= dto.EndDate;
            }

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}