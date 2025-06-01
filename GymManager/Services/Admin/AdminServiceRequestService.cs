using GymManager.Data;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly AdminServiceRequestMapper _mapper;

        public AdminServiceRequestService(
            GymDbContext context,
            AdminServiceRequestMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadServiceRequestDto>> GetAllAsync()
        {
            var list = await _context.ServiceRequests.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadServiceRequestDto?> GetByIdAsync(int id)
        {
            var e = await _context.ServiceRequests.FindAsync(id);
            return e == null
                ? null
                : _mapper.ToReadDto(e);
        }

        public async Task<ReadServiceRequestDto> CreateAsync(CreateServiceRequestDto dto)
        {
            dto.RequestDate = DateTime.Now;
            var e = _mapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateServiceRequestDto dto)
        {
            var e = await _context.ServiceRequests.FindAsync(id);
            if (e == null) return false;
            if(dto.ProblemNote == null) dto.ProblemNote = e.ProblemNote;
            if(dto.ServiceProblemTitle == null) dto.ServiceProblemTitle = e.ServiceProblemTitle;
            if(dto.EquipmentId == null) dto.EquipmentId = e.EquipmentId;
            
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.ServiceRequests.FindAsync(id);
            if (e == null) return false;
            _context.ServiceRequests.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
