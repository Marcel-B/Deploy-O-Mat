using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts
{
    public interface IInspectRRepository
    {
        DockerService AddDockerService(DockerService dockerService);
        DockerServiceDetail AddDockerServiceDetail(DockerServiceDetail dockerServiceDetail);
        Task<DockerService> UpdateDockerService(DockerService dockerService);
        IQueryable<DockerService> DockerServices();
        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}