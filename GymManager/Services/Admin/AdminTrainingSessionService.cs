using GymManager.Data;
using GymManager.Models.DTOs.Admin;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Admin
{
    public class AdminTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainingSessionMapper _mapper;

        public AdminTrainingSessionService(
            GymDbContext context,
            AdminTrainingSessionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            var list = await _context.TrainingSessions.ToListAsync();
            return _mapper.ToReadDtoList(list);
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            return e is null ? null : _mapper.ToReadDto(e);
        }

        public async Task<ReadTrainingSessionDto> CreateAsync(CreateTrainingSessionDto dto)
        {
            var e = _mapper.ToEntity(dto);
            
            var trainerTimeConflict = await _context.TrainingSessions.AnyAsync(a =>
                a.TrainerId == dto.TrainerId &&
                dto.StartTime < a.StartTime.AddMinutes(a.DurationInMinutes) &&
                dto.StartTime.AddMinutes(dto.DurationInMinutes) > a.StartTime);

            if (trainerTimeConflict)
            {
                throw new Exception("Trainer has other session during that time");
            }
            
            if (e.IsGroupSession && e.MemberId != null)
            {
                throw new Exception("You cannot create a group session");
            }

            if (!e.IsGroupSession)
            {
                if (e.MemberId == null)
                { 
                    throw new Exception("You have to choose a member for individual training session");
                }
                else
                {
                    var assignment = await _context.TrainerAssignments.AnyAsync(a => a.MemberId == e.MemberId
                        && a.TrainerId == e.TrainerId
                        && (e.StartTime >= a.StartDate 
                            && e.StartTime.AddMinutes(e.DurationInMinutes) <= a.EndDate));

                    if (!assignment)
                    {
                        throw new Exception("Trainer and member does not have an active assignment.");
                    }
                    
                    var getMembership = await _context.Memberships
                        .FirstOrDefaultAsync(m => m.MemberId == e.MemberId && m.IsActive);

                    if (getMembership == null)
                        throw new Exception("No active membership found.");

                    var getMembershipType = await _context.MembershipTypes
                        .FindAsync(getMembership.MembershipTypeId);

                    if (getMembershipType == null)
                        throw new Exception("Membership type not found.");

                    var userMembershipStartDate = getMembership.StartDate;
                    var daysDifference = (DateTime.Now - userMembershipStartDate).Days;
                    int cycleLength = getMembershipType.DurationInDays;

                    int cycle = (int)Math.Ceiling((double)daysDifference / cycleLength);
                    if (cycle == 0) cycle = 1; // Handle case where DateTime.Now == StartDate

                    var cycleStart = userMembershipStartDate.AddDays((cycle - 1) * cycleLength);
                    var cycleEnd = userMembershipStartDate.AddDays(cycle * cycleLength);

                    int countCurrent = await _context.TrainingSessions
                        .CountAsync(a =>
                            a.MemberId == e.MemberId &&
                            !a.IsGroupSession &&
                            a.StartTime >= cycleStart &&
                            a.StartTime < cycleEnd);

                    if (countCurrent >= getMembershipType.PersonalTrainingsPerMonth)
                    {
                        throw new Exception("You have reached your personal training limit for this membership period.");
                    }
                }

            }
            
            await _context.TrainingSessions.AddAsync(e);
            await _context.SaveChangesAsync();
            return _mapper.ToReadDto(e);
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainingSessionDto dto)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return false;
            _mapper.UpdateEntity(dto, e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return false;
            _context.TrainingSessions.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
