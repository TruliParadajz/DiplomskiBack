using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<BackendApi.Entities.EventTask> EventTasks { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<EventTaskNotification> EventTaskNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTaskNotification>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<EventTaskNotification>()
                .HasIndex(i => i.UserId)
                .IsUnique(false);
            modelBuilder.Entity<EventTaskNotification>()
                .HasIndex(i => i.UserNotificationId)
                .IsUnique(false);

            modelBuilder.Entity<UserNotification>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }

    }
}
