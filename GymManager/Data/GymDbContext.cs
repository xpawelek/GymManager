using GymManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Data;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {}
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProgressPhoto> ProgressPhotos { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<TrainerAssignments> TrainerAssignments { get; set; }
    public DbSet<TrainingSession> TrainingSessions { get; set; }
    public DbSet<WorkoutNote> WorkoutNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TrainingSession>()
            .HasOne(ts => ts.WorkoutNote)
            .WithOne(wn => wn.TrainingSession)
            .HasForeignKey<WorkoutNote>(wn => wn.TrainingSessionId);
    }
}