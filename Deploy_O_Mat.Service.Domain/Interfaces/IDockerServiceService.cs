using System.Collections.Generic;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Domain.Interfaces
{
    public interface IDockerServiceService
    {
        Task<int> UpdateService(string image, string service);
        Task<int> Remove(string service);
        Task<int> Create(DockerService service);
    }
}
