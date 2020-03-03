using BackendApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<BackendApi.Entities.EventTask> EventTask { get; set; }
    }
}
