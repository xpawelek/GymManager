﻿using System.Security.Claims;
using GymManager.Data;
using GymManager.Exceptions;
using GymManager.Shared.DTOs.Member;
using GymManager.Models.Entities;
using GymManager.Models.Mappers.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManager.Services.Member
{
    public class MemberTrainingSessionService
    {
        private readonly GymDbContext _context;
        private readonly MemberTrainingSessionMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<MemberTrainingSessionService> _logger;

        public MemberTrainingSessionService(
            GymDbContext context,
            MemberTrainingSessionMapper mapper,
            IHttpContextAccessor httpContext,
            ILogger<MemberTrainingSessionService> logger)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _logger = logger;
        }

        private async Task<int> GetCurrentMemberId()
        {
            try
            {
                var userIdStr = _httpContext.HttpContext!
                    .User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                var userId = Guid.Parse(userIdStr);

                var member = await _context.Members.FirstOrDefaultAsync(m => m.UserId == userId.ToString());

                if (member == null)
                    throw new UserFacingException("Member not found");

                return member.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get current member ID");
                throw;
            }
        }

        public async Task<List<ReadTrainingSessionDto>> GetAllGroupAsync()
        {
            try
            {
                var list = await _context.TrainingSessions
                    .Where(ts => ts.IsGroupSession || ts.MemberId == null)
                    .Include(ts => ts.Trainer)
                    .ToListAsync();
                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get group sessions");
                throw;
            }
        }

        public async Task<List<ReadTrainingSessionDto>> GetMemberAllPersonalAsync()
        {
            try
            {
                var memberId = await GetCurrentMemberId();

                var hasAssignment = await _context.TrainerAssignments
                    .Include(t => t.Trainer)
                    .AnyAsync(a => a.MemberId == memberId);

                if (!hasAssignment)
                {
                    throw new UserFacingException("Member does not have subscription that allows personal trainings");
                }

                var list = await _context.TrainingSessions
                    .Include(ts => ts.Trainer)
                    .Where(ts => !ts.IsGroupSession && ts.MemberId == memberId)
                    .OrderByDescending(ts => ts.StartTime)
                    .ToListAsync();

                return _mapper.ToReadDtoList(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get personal training sessions");
                throw;
            }
        }

        public async Task<ReadTrainingSessionDto> CreateAsync(CreateTrainingSessionDto dto)
        {
            try
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
                    throw new UserFacingException("Trainer has another session during that time");

                var assignment = await _context.TrainerAssignments
                    .Where(a => a.MemberId == memberId && dto.StartTime >= a.StartDate && a.IsActive)
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

                var membershipType = await _context.MembershipTypes.FindAsync(membership.MembershipTypeId);

                if (membershipType == null)
                    throw new UserFacingException("Membership type not found");

                // limit sesji
                var startDate = membership.StartDate;
                var daysElapsed = (DateTime.Now - startDate).Days;
                int cycleLength = membershipType.DurationInDays;
                int cycle = Math.Max(1, (int)Math.Ceiling((double)daysElapsed / cycleLength));

                var cycleStart = startDate.AddDays((cycle - 1) * cycleLength);
                var cycleEnd = startDate.AddDays(cycle * cycleLength);

                int countCurrent = await _context.TrainingSessions.CountAsync(a =>
                    a.MemberId == memberId && !a.IsGroupSession &&
                    a.StartTime >= cycleStart && a.StartTime < cycleEnd);

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
                    Description = $"{member.FirstName} {member.LastName}, your training with your trainer {trainer.FirstName} {trainer.LastName} will take place on {dto.StartTime:dd-MM-yyyy} at {dto.StartTime:HH:mm}"
                };

                using var transaction = await _context.Database.BeginTransactionAsync();

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create training session");
                throw;
            }
        }

        public async Task<bool> PatchAsync(int sessionId, UpdateTrainingSessionDto dto)
        {
            try
            {
                var memberId = await GetCurrentMemberId();

                var session = await _context.TrainingSessions
                    .FirstOrDefaultAsync(ts => ts.Id == sessionId && ts.MemberId == memberId && !ts.IsGroupSession);

                if (session == null)
                    throw new UserFacingException("Training session not found or access denied");

                if (dto.StartTime == null)
                    throw new UserFacingException("New start time is required");

                var newStartTime = dto.StartTime.Value;

                if (newStartTime < DateTime.Now)
                    throw new UserFacingException("You cannot move a training to the past");

                // przypisanie trenera
                var assignment = await _context.TrainerAssignments
                    .FirstOrDefaultAsync(a => a.MemberId == memberId &&
                                              a.TrainerId == session.TrainerId &&
                                              a.IsActive &&
                                              newStartTime >= a.StartDate &&
                                              newStartTime.AddMinutes(session.DurationInMinutes) <= a.EndDate);

                if (assignment == null)
                    throw new UserFacingException("You are not assigned to this trainer at the selected time");

                // obecny membership
                var membership = await _context.Memberships
                    .FirstOrDefaultAsync(m => m.MemberId == memberId && m.IsActive);

                if (membership == null)
                    throw new UserFacingException("No active membership");

                var membershipType = await _context.MembershipTypes.FindAsync(membership.MembershipTypeId);

                if (membershipType == null)
                    throw new UserFacingException("Membership type not found");

                // limit 
                var startDate = membership.StartDate;
                var daysElapsed = (DateTime.Now - startDate).Days;
                int cycleLength = membershipType.DurationInDays;
                int cycle = Math.Max(1, (int)Math.Ceiling((double)daysElapsed / cycleLength));

                var cycleStart = startDate.AddDays((cycle - 1) * cycleLength);
                var cycleEnd = startDate.AddDays(cycle * cycleLength);

                int countCurrent = await _context.TrainingSessions
                    .CountAsync(ts => ts.Id != session.Id && ts.MemberId == memberId &&
                                      !ts.IsGroupSession && ts.StartTime >= cycleStart && ts.StartTime < cycleEnd);

                if (countCurrent >= membershipType.PersonalTrainingsPerMonth)
                    throw new UserFacingException("You have reached your personal training limit for this cycle");

                // czy ma slot
                var trainerConflict = await _context.TrainingSessions.AnyAsync(ts =>
                    ts.Id != session.Id && ts.TrainerId == session.TrainerId &&
                    newStartTime < ts.StartTime.AddMinutes(ts.DurationInMinutes) &&
                    newStartTime.AddMinutes(session.DurationInMinutes) > ts.StartTime);

                if (trainerConflict)
                    throw new UserFacingException("Trainer is already booked for the selected time");

                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId);
                var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == assignment.TrainerId);

                session.StartTime = newStartTime;
                session.Description = $"{member.FirstName} {member.LastName}, your training with your trainer {trainer.FirstName} {trainer.LastName} will take place on {dto.StartTime:dd-MM-yyyy} at {dto.StartTime:HH:mm}";

                var workoutNote = await _context.WorkoutNotes.FirstOrDefaultAsync(w => w.TrainingSessionId == session.Id);
                if (workoutNote != null)
                {
                    workoutNote.WorkoutStartTime = newStartTime;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update training session");
                throw;
            }
        }

        public async Task<ReadTrainingSessionDto?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _context.TrainingSessions.FindAsync(id);
                return e == null ? null : _mapper.ToReadDto(e);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get training session by ID: {id}");
                throw;
            }
        }

        public async Task<bool> DeleteOwnAsync(int id)
        {
            try
            {
                var e = await _context.TrainingSessions.FindAsync(id);
                if (e == null) return false;

                if (e.MemberId != await GetCurrentMemberId())
                    throw new UserFacingException("Cannot delete others' training session");

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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete training session with ID: {id}");
                throw;
            }
        }
    }
}