using Microsoft.EntityFrameworkCore;
using QWIIK.ProjectTest.Entity;

namespace QWIIK.ProjectTest.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AppointmentsEntity> Appointments { get; set; }
        public DbSet<AppointmentOptions> AppointmentOptions { get; set; }
        public DbSet<UserAppointments> UserAppoinments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<AppointmentsEntity>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("date");
                entity.HasIndex(e => e.AppointmentDate).IsUnique();
                entity.HasQueryFilter(e => !e.IsDeleted);
            });

            modelBuilder.Entity<UserAppointments>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("date");
                entity.HasIndex(e => e.AppointmentDate);
                entity.HasIndex(e => e.UserId);
                entity.HasQueryFilter(e => !e.IsDeleted);
            });
        }
    }
}
