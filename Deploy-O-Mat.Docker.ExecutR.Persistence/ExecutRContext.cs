using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence
{
    public class ExecutRContext : DbContext
    {
        public ExecutRContext(DbContextOptions<ExecutRContext> builder) :base(builder)
        {
            
        }
    }
}