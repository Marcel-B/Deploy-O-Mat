using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context
{
    public class InspectRContext : DbContext
    {
        public DbSet<DockerService> DockerServices { get; set; }
        public DbSet<Argument> Arguments { get; set; }
        public DbSet<DockerServiceDetail> DockerServiceDetails { get; set; }
        public DbSet<EnvironmentVariable> EnvironmentVariables { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Mount> Mounts { get; set; }
        public DbSet<Placement> Placements { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<VirtualIp> VirtualIps { get; set; }
        public DbSet<VolumeOption> VolumeOptions { get; set; }
        
        public InspectRContext(DbContextOptions<InspectRContext> options) : base(options)
        {
        }
    }
}