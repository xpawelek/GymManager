using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using GymManager.Models.Mappers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace GymManager.Services.Trainer
{
    public class TrainerServiceRequestService
    {
        private readonly GymDbContext _context;
        private readonly TrainerServiceRequestMapper _createMapper;

        public TrainerServiceRequestService(
            GymDbContext context,
            TrainerServiceRequestMapper createMapper,
            AdminServiceRequestMapper readMapper)
        {
            _context = context;
            _createMapper = createMapper;
        }

        public async Task<bool> CreateAsync(CreateServiceRequestDto dto)
        {
            dto.RequestDate = DateTime.Now;
            var entity = _createMapper.ToEntity(dto);
            await _context.ServiceRequests.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
