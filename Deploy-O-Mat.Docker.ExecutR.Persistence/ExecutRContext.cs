using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence
{
    public class ExecutRContext : DbContext
    {
        public DbSet<CommandLog> CommandLogs { get; set; }
        public ExecutRContext(DbContextOptions<ExecutRContext> builder) :base(builder)
        {
            
        }
    }
}