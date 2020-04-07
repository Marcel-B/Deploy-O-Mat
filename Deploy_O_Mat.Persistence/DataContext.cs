using com.b_velop.Deploy_O_Mat.Domain;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(
            DbContextOptions options) : base(options) { }

        public DbSet<DockerImage> DockerImages { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
