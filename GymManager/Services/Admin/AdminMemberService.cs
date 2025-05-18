using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin;

public class AdminMemberService
{
    private readonly GymDbContext _context;
    private readonly AdminMemberMapper _mapper;

    public AdminMemberService(GymDbContext context, AdminMemberMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReadMemberDto>> GetAllAsync()
    {
        var members = await _context.Members.ToListAsync();
        return _mapper.ToReadDtoList(members);
    }

    public async Task<ReadMemberDto> GetByIdAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        return _mapper.ToReadDto(member!);
    }

    public async Task<ReadMemberDto> CreateAsync(CreateMemberDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.Members.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.ToReadDto(entity);
    }

    public async Task<bool> PatchAsync(int id, UpdateMemberDto dto)
    {
        var entity = await _context.Members.FindAsync(id);
        if (entity == null) return false;

        if (dto.FirstName is not null) entity.FirstName = dto.FirstName;
        if (dto.LastName is not null) entity.LastName = dto.LastName;
        if (dto.Email is not null) entity.Email = dto.Email;
        if (dto.PhoneNumber is not null) entity.PhoneNumber = dto.PhoneNumber;
        if (dto.DateOfBirth.HasValue) entity.DateOfBirth = dto.DateOfBirth.Value;

        _mapper.UpdateEntity(dto, entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Members.FindAsync(id);
        if (entity == null) return false;

        _context.Members.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
