using Deploy_O_Mat.Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Deploy_O_Mat.Service.Data.Context
{
    public class DockerServiceDbContext : DbContext
    {
        public DockerServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DockerService> DockerServices { get; set; }
    }
}