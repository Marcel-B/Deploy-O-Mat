using System;
using Microsoft.EntityFrameworkCore;

namespace Deploy_O_Mat.Service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DockerService> DockerServices { get; set; }

        //protected override void OnConfiguring(
        //    DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=services.db");
    }
}
