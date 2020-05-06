using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Persistence.Context
{
    public class WebContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public WebContext(
            DbContextOptions<WebContext> options) : base(options) { }

        public DbSet<DockerImage> DockerImages { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<DockerStack> DockerStacks { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<DockerStackLog> DockerStackLogs { get; set; }
        public DbSet<DockerService> DockerServices { get; set; }
        public DbSet<DockerStackImage> DockerStackImages { get; set; }
        public DbSet<DockerActiveService> DockerActiveServices { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestLog>().HasIndex("Created");
            modelBuilder.Entity<DockerStackLog>().HasIndex("Image");

            modelBuilder.Entity<DockerStackImage>(x => x.HasKey(dis => new { dis.DockerImageId, dis.DockerStackId }));
            modelBuilder.Entity<DockerStackImage>()
                .HasOne(i => i.DockerImage)
                .WithMany(s => s.DockerStackImages)
                .HasForeignKey(i => i.DockerImageId);
            modelBuilder.Entity<DockerStackImage>()
                .HasOne(s => s.DockerStack)
                .WithMany(i => i.DockerStackImages)
                .HasForeignKey(s => s.DockerStackId);
        }
    }
}
