using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Repositories
{
    public interface IExecutRRepository
    {
        Task SaveCommandToLog(CommandLog commandLog);
        Task<int> SaveChanges();
    }
}