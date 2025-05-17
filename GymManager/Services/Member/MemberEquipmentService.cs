using GymManager.Data;
using GymManager.Models.DTOs.Member;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member;

public class MemberEquipmentService
{
    private readonly GymDbContext _context;
    private readonly MemberEquipmentMapper _mapper;

    public MemberEquipmentService(GymDbContext context, MemberEquipmentMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReadEquipmentDto>> GetAllAsync()
    {
        var equipment = await _context.Equipments.ToListAsync();
        return _mapper.ToReadDtoList(equipment);
    }
}