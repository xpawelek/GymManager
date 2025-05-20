using System.Security.Claims;
using GymManager.Data;
using GymManager.Models.DTOs.Trainer;
using GymManager.Models.Mappers.Trainer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Trainer
{
    public class TrainerSelfTrainerAssignmentService
    {
        private readonly GymDbContext _context;
        private readonly TrainerSelfTrainerAssignmentMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public TrainerSelfTrainerAssignmentService(
            GymDbContext context,
            TrainerSelfTrainerAssignmentMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
        }

        private int GetCurrentTrainerId() =>
            int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<List<ReadSelfTrainerAssignmentDto>> GetAllAsync()
        {
            var tid = GetCurrentTrainerId();
            var list = await _context.TrainerAssignments
                                     .Where(a => a.TrainerId == tid)
                                     .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadSelfTrainerAssignmentDto?> GetByIdAsync(int id)
        {
            var tid = GetCurrentTrainerId();
            var e = await _context.TrainerAssignments.FindAsync(id);
            if (e == null || e.TrainerId != tid) return null;
            return _mapper.ToReadDto(e);
        }
    }
}
