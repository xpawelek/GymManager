using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer;

public class TrainerEquipmentService
{
    private readonly GymDbContext _context;
    private readonly TrainerEquipmentMapper _mapper;

    public TrainerEquipmentService(GymDbContext context, TrainerEquipmentMapper mapper)
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