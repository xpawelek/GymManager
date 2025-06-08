using System.Security.Claims;
using GymManager.Data;
using GymManager.Exceptions;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Services.Member
{
    public class MemberTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainingSessionMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public MemberTrainingSessionService(
            GymDbContext context,
            MemberTrainingSessionMapper mapper,
            IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        
        private async Task<int> GetCurrentMemberId()
        {
            var userIdStr = _httpContext.HttpContext!
                .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var userId = Guid.Parse(userIdStr); 

            var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId.ToString());

            if (member == null)
                throw new Exception("Member not found");

            return member.Id;
        }
        public async Task<List<ReadTrainingSessionDto>> GetAllGroupAsync()
        {
            var list = await _context.TrainingSessions
                .Where(ts => ts.IsGroupSession || ts.MemberId == null)
                .ToListAsync();
            return _mapper.ToReadDtoList(list);
        }
        
        public async Task<List<ReadTrainingSessionDto>> GetMemberAllPersonalAsync()
        {
            var memberId = await GetCurrentMemberId();

            var hasAssignment = await _context.TrainerAssignments
                .Include(t => t.Trainer)
                .AnyAsync(a => a.MemberId == memberId);
            
            if (!hasAssignment)
            {
                throw new Exception("Member does not have subscription that allows personal trainings");
            }

            var list = await _context.TrainingSessions
                .Include(ts => ts.Trainer)
                .Where(ts => !ts.IsGroupSession && ts.MemberId == memberId)
                .OrderByDescending(ts => ts.StartTime) 
                .ToListAsync();

            return _mapper.ToReadDtoList(list);
        }
        
        
        public async Task<ReadTrainingSessionDto> CreateAsync(CreateTrainingSessionDto dto)
        {
            var memberId = await GetCurrentMemberId();

            // Validate basic input
            if (dto.StartTime < DateTime.Now)
                throw new UserFacingException("You cannot schedule a session in the past");
            
            var trainerTimeConflict = await _context.TrainingSessions.AnyAsync(a =>
                a.TrainerId == dto.TrainerId &&
                dto.StartTime < a.StartTime.AddMinutes(a.DurationInMinutes) &&
                dto.StartTime.AddMinutes(dto.DurationInMinutes) > a.StartTime);

            if (trainerTimeConflict)
            {
                throw new UserFacingException("Trainer has another session during that time");
            }

            var assignment = await _context.TrainerAssignments
                .Where(a => a.MemberId == memberId &&
                            dto.StartTime >= a.StartDate &&
                            a.IsActive)
                .OrderBy(a => a.StartDate)
                .FirstOrDefaultAsync();

            if (assignment == null)
                throw new UserFacingException("You don't have a valid trainer assignment for that date");

            dto.TrainerId = assignment.TrainerId;
            
            // Check active membership
            var membership = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

            if (membership == null)
                throw new UserFacingException("You don't have an active membership");

            var membershipType = await _context.MembershipTypes
                .FindAsync(membership.MembershipTypeId);

            if (membershipType == null)
                throw new UserFacingException("Membership type not found");

            // limit sesji
            var startDate = membership.StartDate;
            var daysElapsed = (DateTime.Now - startDate).Days;
            int cycleLength = membershipType.DurationInDays;

            int cycle = (int)Math.Ceiling((double)daysElapsed / cycleLength);
            if (cycle == 0) cycle = 1;

            var cycleStart = startDate.AddDays((cycle - 1) * cycleLength);
            var cycleEnd = startDate.AddDays(cycle * cycleLength);

            int countCurrent = await _context.TrainingSessions.CountAsync(a =>
                a.MemberId == memberId &&
                !a.IsGroupSession &&
                a.StartTime >= cycleStart &&
                a.StartTime < cycleEnd);

            if (countCurrent >= membershipType.PersonalTrainingsPerMonth)
                throw new UserFacingException("You’ve reached your personal training session limit for this cycle");

            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId);
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == assignment.TrainerId);
            
            var session = new TrainingSession
            {
                TrainerId = dto.TrainerId,
                MemberId = memberId,
                StartTime = dto.StartTime,
                DurationInMinutes = 120,
                IsGroupSession = false,
                Description = $"{member.FirstName} {member.LastName}, " +
                              $"your training with your trainer {trainer.FirstName} {trainer.LastName} will take place" +
                              $"on {dto.StartTime:dd-MM-yyyy} at {dto.StartTime:HH:mm}",
            };

            var transaction = await _context.Database.BeginTransactionAsync();

            await _context.TrainingSessions.AddAsync(session);
            await _context.SaveChangesAsync();

            // dodanie notki
            var workoutNote = new WorkoutNote
            {
                TrainingSessionId = session.Id,
                MemberId = memberId,
                TrainerId = session.TrainerId,
                WorkoutInfo = string.Empty,
                WorkoutStartTime = session.StartTime
            };

            await _context.WorkoutNotes.AddAsync(workoutNote);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return _mapper.ToReadDto(session);
        }
        
        public async Task<bool> PatchAsync(int sessionId, UpdateTrainingSessionDto dto)
        {
            var memberId = await GetCurrentMemberId();

            var session = await _context.TrainingSessions
                .FirstOrDefaultAsync(ts => ts.Id == sessionId && ts.MemberId == memberId && !ts.IsGroupSession);

            if (session == null)
                throw new Exception("Training session not found or access denied");

            if (dto.StartTime == null)
                throw new Exception("New start time is required");

            var newStartTime = dto.StartTime.Value;

            if (newStartTime < DateTime.Now)
                throw new Exception("You cannot move a training to the past");

            // przypisanie trenera
            var assignment = await _context.TrainerAssignments
                .FirstOrDefaultAsync(a => a.MemberId == memberId &&
                                          a.TrainerId == session.TrainerId &&
                                          a.IsActive &&
                                          newStartTime >= a.StartDate &&
                                          newStartTime.AddMinutes(session.DurationInMinutes) <= a.EndDate);

            if (assignment == null)
                throw new Exception("You are not assigned to this trainer at the selected time");

            // obecny membership
            var membership = await _context.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

            if (membership == null)
                throw new Exception("No active membership");

            var membershipType = await _context.MembershipTypes
                .FindAsync(membership.MembershipTypeId);

            if (membershipType == null)
                throw new Exception("Membership type not found");

            // limit 
            var startDate = membership.StartDate;
            var daysElapsed = (DateTime.Now - startDate).Days;
            int cycleLength = membershipType.DurationInDays;

            int cycle = (int)Math.Ceiling((double)daysElapsed / cycleLength);
            if (cycle == 0) cycle = 1;

            var cycleStart = startDate.AddDays((cycle - 1) * cycleLength);
            var cycleEnd = startDate.AddDays(cycle * cycleLength);

            int countCurrent = await _context.TrainingSessions
                .CountAsync(ts =>
                    ts.Id != session.Id &&
                    ts.MemberId == memberId &&
                    !ts.IsGroupSession &&
                    ts.StartTime >= cycleStart &&
                    ts.StartTime < cycleEnd);

            if (countCurrent >= membershipType.PersonalTrainingsPerMonth)
                throw new Exception("You have reached your personal training limit for this cycle");

            // czy ma slot
            var trainerConflict = await _context.TrainingSessions.AnyAsync(ts =>
                ts.Id != session.Id &&
                ts.TrainerId == session.TrainerId &&
                newStartTime < ts.StartTime.AddMinutes(ts.DurationInMinutes) &&
                newStartTime.AddMinutes(session.DurationInMinutes) > ts.StartTime);

            if (trainerConflict)
                throw new Exception("Trainer is already booked for the selected time");
            
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId);
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == assignment.TrainerId);
            
            session.StartTime = newStartTime;
            session.Description = $"{member.FirstName} {member.LastName}, " +
                                  $"your training with your trainer {trainer.FirstName} {trainer.LastName} will take place" +
                                  $"on {dto.StartTime:dd-MM-yyyy} at {dto.StartTime:HH:mm}";

            var workoutNote = await _context.WorkoutNotes.FirstOrDefaultAsync(w => w.TrainingSessionId == session.Id);
            if (workoutNote != null)
            {
                workoutNote.WorkoutStartTime = newStartTime;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return null;
            return _mapper.ToReadDto(e);
        }
        
        
        public async Task<bool> DeleteOwnAsync(int id)
        {
            var e = await _context.TrainingSessions.FindAsync(id);
            if (e == null) return false;

            if (e.MemberId != await GetCurrentMemberId())
            {
                throw new Exception("Cannot delete others training session");
            }

            var workoutNote = await _context.WorkoutNotes
                .FirstOrDefaultAsync(w => w.TrainingSessionId == id);

            if (workoutNote != null)
            {
                _context.WorkoutNotes.Remove(workoutNote);
            }

            _context.TrainingSessions.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
