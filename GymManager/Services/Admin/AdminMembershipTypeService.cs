using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin;

public class AdminMembershipTypeService
{
    private readonly GymDbContext _context;
    private readonly AdminMembershipTypeMapper _mapper;

    public AdminMembershipTypeService(
        GymDbContext context,
        AdminMembershipTypeMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReadMembershipTypeDto>> GetAllAsync()
    {
        var list = await _context.MembershipTypes.ToListAsync();
        return _mapper.ToReadDtoList(list);
    }

    public async Task<ReadMembershipTypeDto> GetByIdAsync(int id)
    {
        var entity = await _context.MembershipTypes.FindAsync(id);
        return _mapper.ToReadDto(entity!);
    }

    public async Task<ReadMembershipTypeDto> CreateAsync(CreateMembershipTypeDto dto)
    {
        var entity = _mapper.ToEntity(dto);
        await _context.MembershipTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.ToReadDto(entity);
    }

    public async Task<bool> PatchAsync(int id, UpdateMembershipTypeDto dto)
    {
        var entity = await _context.MembershipTypes.FindAsync(id);
        if (entity == null) return false;
        if(dto.PersonalTrainingsPerWeek == null) dto.PersonalTrainingsPerWeek = entity.PersonalTrainingsPerWeek;
        _mapper.UpdateEntity(dto, entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.MembershipTypes.FindAsync(id);
        if (entity == null) return false;
        _context.MembershipTypes.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}