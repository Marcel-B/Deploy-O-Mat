using com.b_velop.Deploy_O_Mat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(
            DbContextOptions options) : base(options) { }

        public DbSet<DockerImage> DockerImages { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<DockerStackService> DockerStackServices { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestLog>().HasIndex("Created");
        }
    }
}
