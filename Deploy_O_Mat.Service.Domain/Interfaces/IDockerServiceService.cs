using System.Collections.Generic;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Domain.Interfaces
{
    public interface IDockerServiceService
    {
        IEnumerable<DockerService> GetDockerServices();
        Task<int> UpdateService(DockerService service);
        Task<int> StopService(string service);
        Task<int> StartService(string service);
    }
}
