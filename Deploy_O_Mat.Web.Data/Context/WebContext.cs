using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Context
{
    public class WebContext : DbContext
    {
        public WebContext(
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
