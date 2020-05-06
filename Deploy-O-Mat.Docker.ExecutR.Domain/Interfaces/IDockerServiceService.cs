using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces
{
    public interface IDockerServiceService
    {
        Task<int> UpdateService(string image, string service);
        Task<int> Remove(string service);
        Task<int> Create(DockerService service);
    }
}
