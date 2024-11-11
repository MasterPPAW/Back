using LibrarieModele;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Configuration;

namespace Repository_CodeFirst
{
    public class FitnessDBContext : DbContext
    {
        public FitnessDBContext()
        {
        }

        public FitnessDBContext(DbContextOptions<FitnessDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<NewModel> NewModels { get; set; }

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasKey(wpe => new { wpe.PlanId, wpe.ExerciseId });

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasOne(wpe => wpe.WorkoutPlan)
                .WithMany(wp => wp.WorkoutPlanExercises)
                .HasForeignKey(wpe => wpe.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkoutPlanExercise>()
                .HasOne(wpe => wpe.Exercise)
                .WithMany(e => e.WorkoutPlanExercises)
                .HasForeignKey(wpe => wpe.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Property(u => u.RegistrationDate)
                .HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Subscription>()
                .Property(s => s.StartDate)
                .HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentDate)
                .HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Subscription)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql(ConfigurationManager.ConnectionStrings["FitnessDBConnection"].ConnectionString)
                    .UseLazyLoadingProxies();
                    //.EnableSensitiveDataLogging()
                    //.LogTo(Console.WriteLine, LogLevel.Information);
            }
        }
    }
}
