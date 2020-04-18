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
    }
}
