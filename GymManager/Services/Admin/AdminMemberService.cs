using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Identity;
using GymManager.Models.Mappers.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin;

public class AdminMemberService
{
    private readonly GymDbContext _context;
    private readonly AdminMemberMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AdminMemberService> _logger;

    public AdminMemberService(
        GymDbContext context,
        AdminMemberMapper mapper,
        UserManager<ApplicationUser> userManager,
        ILogger<AdminMemberService> logger)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<List<ReadMemberDto>> GetAllAsync()
    {
        try
        {
            var members = await _context.Members.Include(m => m.User).ToListAsync();
            return _mapper.ToReadDtoList(members);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving all members.");
            return new List<ReadMemberDto>();
        }
    }

    public async Task<ReadMemberDto?> GetByIdAsync(int id)
    {
        try
        {
            var member = await _context.Members.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);
            return member == null ? null : _mapper.ToReadDto(member);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while retrieving member with ID {Id}.", id);
            return null;
        }
    }

    /*
    public async Task<ReadMemberDto> CreateAsync(CreateMemberDto dto)
    {
        try
        {
            var entity = _mapper.ToEntity(dto);
            await _context.Members.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating new member.");
            return null;
        }
    }
    */

    public async Task<bool> PatchAsync(int id, UpdateMemberDto dto)
    {
        try
        {
            var entity = await _context.Members.FindAsync(id);
            if (entity == null) return false;

            if (dto.FirstName is not null) entity.FirstName = dto.FirstName;
            if (dto.LastName is not null) entity.LastName = dto.LastName;
            if (dto.PhoneNumber is not null) entity.PhoneNumber = dto.PhoneNumber;
            if (dto.DateOfBirth.HasValue) entity.DateOfBirth = dto.DateOfBirth.Value;

            _mapper.UpdateEntity(dto, entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating member with ID {Id}.", id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _context.Members
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (entity == null) return false;

            _context.Members.Remove(entity);

            if (entity.User != null)
            {
                var result = await _userManager.DeleteAsync(entity.User);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to delete ApplicationUser for member with ID {Id}.", id);
                    return false;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting member with ID {Id}.", id);
            return false;
        }
    }
}
