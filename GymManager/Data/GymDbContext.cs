using GymManager.Models.Entities;
using GymManager.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManager.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GymManager.Models.Identity;

public class GymDbContext : IdentityDbContext<ApplicationUser>
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) {}
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
        
        modelBuilder.Entity<Trainer>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Member>()
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}