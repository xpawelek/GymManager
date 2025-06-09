using GymManager.Data;
using GymManager.Exceptions;
using GymManager.Shared.DTOs.Admin;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Admin
{
    public class AdminTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly AdminTrainingSessionMapper _mapper;
        private readonly ILogger<AdminTrainingSessionService> _logger;

        public AdminTrainingSessionService(
            GymDbContext context,
            AdminTrainingSessionMapper mapper,
            ILogger<AdminTrainingSessionService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllAsync()
        {
            try
            {
                var list = await _context.TrainingSessions.Include(t => t.Trainer).ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all training sessions.");
                return new List<ReadTrainingSessionDto>();
            }
        }

        public async Task<List<ReadTrainingSessionDto>> GetByMemberIdAsync(int id)
        {
            try
            {
                var sessions = await _context.TrainingSessions
                    .Include(s => s.Trainer)
                    .Where(s => s.MemberId == id && s.StartTime >= DateTime.Now)
                    .OrderBy(s => s.StartTime)
                    .ToListAsync();

                return _mapper.ToReadDtoList(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving training sessions for member ID {Id}.", id);
                return new List<ReadTrainingSessionDto>();
            }
        }

        public async Task<List<ReadTrainingSessionDto>> GetByTrainerIdAsync(int id)
        {
            try
            {
                var sessions = await _context.TrainingSessions
                    .Include(s => s.Trainer)
                    .Where(s => s.TrainerId == id && s.StartTime >= DateTime.Now)
                    .OrderBy(s => s.StartTime)
                    .ToListAsync();

                return _mapper.ToReadDtoList(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving training sessions for trainer ID {Id}.", id);
                return new List<ReadTrainingSessionDto>();
            }
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.TrainingSessions.FindAsync(id);
                return e is null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving training session with ID {Id}.", id);
                return null;
            }
        }

        public async Task<ReadTrainingSessionDto?> CreateAsync(CreateTrainingSessionDto dto)
        {
            try
            {
                var e = _mapper.ToEntity(dto);

                if (e.StartTime < DateTime.Now)
                    throw new UserFacingException("You cannot create a training session in the past");

                var trainerConflict = await _context.TrainingSessions.AnyAsync(a =>
                    a.TrainerId == dto.TrainerId &&
                    dto.StartTime < a.StartTime.AddMinutes(a.DurationInMinutes) &&
                    dto.StartTime.AddMinutes(dto.DurationInMinutes) > a.StartTime);

                if (trainerConflict)
                    throw new UserFacingException("Trainer has another session during that time");

                if (e.IsGroupSession && e.MemberId != null)
                    throw new UserFacingException("You cannot create a group session with a member");

                if (!e.IsGroupSession)
                {
                    if (e.MemberId == null)
                        throw new UserFacingException("You must choose a member for individual training");

                    var assignment = await _context.TrainerAssignments.AnyAsync(a =>
                        a.MemberId == e.MemberId && a.TrainerId == e.TrainerId &&
                        e.StartTime >= a.StartDate && e.StartTime.AddMinutes(e.DurationInMinutes) <= a.EndDate);

                    if (!assignment)
                        throw new UserFacingException("Trainer and member do not have an active assignment");

                    var membership = await _context.Memberships
                        .FirstOrDefaultAsync(m => m.MemberId == e.MemberId && m.IsActive);

                    if (membership == null)
                        throw new UserFacingException("No active membership found");

                    var type = await _context.MembershipTypes.FindAsync(membership.MembershipTypeId);
                    if (type == null)
                        throw new UserFacingException("Membership type not found");

                    var cycleLength = type.DurationInDays;
                    var daysPassed = (DateTime.Now - membership.StartDate).Days;
                    var cycle = Math.Max(1, (int)Math.Ceiling((double)daysPassed / cycleLength));

                    var cycleStart = membership.StartDate.AddDays((cycle - 1) * cycleLength);
                    var cycleEnd = membership.StartDate.AddDays(cycle * cycleLength);

                    int count = await _context.TrainingSessions.CountAsync(a =>
                        a.MemberId == e.MemberId &&
                        !a.IsGroupSession &&
                        a.StartTime >= cycleStart && a.StartTime < cycleEnd);

                    if (count >= type.PersonalTrainingsPerMonth)
                        throw new UserFacingException("You have reached your personal training limit for this period.");
                }

                await using var transaction = await _context.Database.BeginTransactionAsync();
                await _context.TrainingSessions.AddAsync(e);
                await _context.SaveChangesAsync();

                if (!e.IsGroupSession)
                {
                    var note = new WorkoutNote
                    {
                        TrainingSessionId = e.Id,
                        MemberId = e.MemberId!.Value,
                        TrainerId = e.TrainerId,
                        WorkoutInfo = string.Empty,
                        WorkoutStartTime = e.StartTime
                    };
                    await _context.WorkoutNotes.AddAsync(note);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return _mapper.ToReadDto(e);
            }
            catch (UserFacingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating training session.");
                return null;
            }
        }

        public async Task<bool> PatchAsync(int id, UpdateTrainingSessionDto dto)
        {
            try
            {
                var e = await _context.TrainingSessions.FindAsync(id);
                if (e == null) return false;

                var newTrainerId = dto.TrainerId ?? e.TrainerId;
                var newStartTime = dto.StartTime ?? e.StartTime;
                var duration = e.DurationInMinutes;

                var trainerConflict = await _context.TrainingSessions.AnyAsync(a =>
                    a.Id != e.Id &&
                    a.TrainerId == newTrainerId &&
                    newStartTime < a.StartTime.AddMinutes(a.DurationInMinutes) &&
                    newStartTime.AddMinutes(duration) > a.StartTime);

                if (trainerConflict)
                    throw new Exception("Trainer has another session during that time");

                if (!e.IsGroupSession)
                {
                    if (e.MemberId == null)
                        throw new Exception("You must choose a member for individual training");

                    var assignment = await _context.TrainerAssignments.AnyAsync(a =>
                        a.MemberId == e.MemberId &&
                        a.TrainerId == newTrainerId &&
                        newStartTime >= a.StartDate &&
                        newStartTime.AddMinutes(duration) <= a.EndDate);

                    if (!assignment)
                        throw new Exception("Trainer and member do not have an active assignment");

                    var membership = await _context.Memberships
                        .FirstOrDefaultAsync(m => m.MemberId == e.MemberId && m.IsActive);

                    if (membership == null)
                        throw new Exception("No active membership found");

                    var type = await _context.MembershipTypes.FindAsync(membership.MembershipTypeId);
                    if (type == null)
                        throw new Exception("Membership type not found");

                    var days = (DateTime.Now - membership.StartDate).Days;
                    var cycle = Math.Max(1, (int)Math.Ceiling((double)days / type.DurationInDays));
                    var cycleStart = membership.StartDate.AddDays((cycle - 1) * type.DurationInDays);
                    var cycleEnd = membership.StartDate.AddDays(cycle * type.DurationInDays);

                    var count = await _context.TrainingSessions.CountAsync(a =>
                        a.Id != e.Id &&
                        a.MemberId == e.MemberId &&
                        !a.IsGroupSession &&
                        a.StartTime >= cycleStart &&
                        a.StartTime < cycleEnd);

                    if (count >= type.PersonalTrainingsPerMonth)
                        throw new Exception("You have reached your personal training limit for this period.");

                    var note = await _context.WorkoutNotes.FirstOrDefaultAsync(w => w.TrainingSessionId == e.Id);
                    if (note == null)
                        throw new Exception("Workout note not found");

                    note.TrainerId = newTrainerId;
                    note.WorkoutStartTime = newStartTime;
                }

                _mapper.UpdateEntity(dto, e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating training session with ID {Id}.", id);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var e = await _context.TrainingSessions.FindAsync(id);
                if (e == null) return false;

                var note = await _context.WorkoutNotes.FirstOrDefaultAsync(w => w.TrainingSessionId == id);
                if (note != null)
                {
                    _context.WorkoutNotes.Remove(note);
                }

                _context.TrainingSessions.Remove(e);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting training session with ID {Id}.", id);
                return false;
            }
        }
    }
}
