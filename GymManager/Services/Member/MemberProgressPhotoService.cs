using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberProgressPhotoService
    {
        private readonly GymDbContext _context;
        private readonly MemberProgressPhotoMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberProgressPhotoService(
            GymDbContext context,
            MemberProgressPhotoMapper mapper,
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

        public async Task<List<ReadProgressPhotoDto>> GetAllAsync()
        {
            var memberId = GetCurrentMemberId()!.Value;
            var list = await _context.ProgressPhotos
                .Where(p => p.MemberId == memberId)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadProgressPhotoDto> GetByIdAsync(int id)
        {
            var memberId = GetCurrentMemberId()!.Value;
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null || entity.MemberId != memberId)
                return null!;
            return _mapper.ToReadDto(entity);
        }

        public async Task<ReadProgressPhotoDto> CreateAsync(CreateProgressPhotoDto dto)
        {
            dto.MemberId = GetCurrentMemberId()!.Value;
            var entity = _mapper.ToEntity(dto);
            await _context.ProgressPhotos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }

        public async Task<bool> PatchAsync(int id, UpdateProgressPhotoDto dto)
        {
            var memberId = GetCurrentMemberId()!.Value;
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null || entity.MemberId != memberId)
                return false;
            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var memberId = GetCurrentMemberId()!.Value;
            var entity = await _context.ProgressPhotos.FindAsync(id);
            if (entity == null || entity.MemberId != memberId)
                return false;
            _context.ProgressPhotos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
