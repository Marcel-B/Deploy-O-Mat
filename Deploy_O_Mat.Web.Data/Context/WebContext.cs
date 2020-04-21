﻿using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Context
{
    public class WebContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole,  IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public WebContext(
            DbContextOptions<WebContext> options) : base(options) { }

        public DbSet<DockerImage> DockerImages { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<DockerStackService> DockerStackServices { get; set; }
        public DbSet<DockerStack> DockerStacks { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<DockerStackLog> DockerStackLogs { get; set; }
        public DbSet<DockerService> DockerServices { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RequestLog>().HasIndex("Created");
            modelBuilder.Entity<DockerStackLog>().HasIndex("Image");
        }
    }
}
