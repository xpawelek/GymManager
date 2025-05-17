using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin;

public class AdminEquipmentService
{
    private readonly GymDbContext _context;
    private readonly AdminEquipmentMapper _mapper;

    public AdminEquipmentService(GymDbContext context, AdminEquipmentMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReadEquipmentDto>> GetAllAsync()
    {
        var equipments = await _context.Equipments.ToListAsync();
        return _mapper.ToReadDtoList(equipments);
    }

    public async Task<ReadEquipmentDto> CreateAsync(CreateEquipmentDto createEquipmentDto)
    {
        var equipment = _mapper.ToEntity(createEquipmentDto);
        await _context.Equipments.AddAsync(equipment);
        await _context.SaveChangesAsync();
        return _mapper.ToReadDto(equipment);
    }
 
    public async Task<bool> PatchAsync(int id, UpdateEquipmentDto dto)
    {
        var entity = await _context.Equipments.FindAsync(id);
        if (entity == null)
            return false;
        
        if(dto.Name is not null) entity.Name = dto.Name;
        if(dto.Description is not null) entity.Description = dto.Description;
        if(dto.Notes is not null) entity.Notes = dto.Notes;
        
        _mapper.UpdateEntity(dto, entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Equipments.FindAsync(id);
        if(entity == null)
            return false;
        
        _context.Equipments.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}