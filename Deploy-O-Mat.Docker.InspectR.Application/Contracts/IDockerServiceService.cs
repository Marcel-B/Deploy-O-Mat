using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts
{
    public interface IDockerServiceService
    {
        Task<IEnumerable<DockerService>> GetDockerServices();
        Task<DockerServiceDetail> GetDockerServiceDetail(string serviceId);
        Task<bool> UpdateDockerServices();
        Task<IEnumerable<DockerServiceDetail>> GetDockerServiceDetails(IEnumerable<string> serviceIds);

    }
}