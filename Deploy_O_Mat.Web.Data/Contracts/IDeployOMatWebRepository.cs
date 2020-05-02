using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Contracts
{
    public interface IDeployOMatWebRepository
    {
        Task CreateOrUpdateDockerStackLog(IEnumerable<DockerStackLog> stackLogs);

        List<DockerStackLog> GetDockerStackLogs();
        Task<IEnumerable<DockerStack>> GetDockerStacks();
        Task<IEnumerable<DockerService>> GetDockerServices();
        Task<IEnumerable<DockerImage>> GetDockerImages(CancellationToken cancellationToken = default);

        DockerImage CreateDockerImage(DockerImage entity, CancellationToken cancellationToken = default);

        Task<DockerImage> GetDockerImage(Guid id, CancellationToken cancellationToken = default);
        Task<DockerStack> GetDockerStack(Guid id);
        Task<DockerService> GetDockerService(Guid id);
        IEnumerable<DockerActiveService> GetDockerActiveServicesByImageId(Guid id);

        DockerImage UpdateDockerImage(DockerImage newDockerImage, DockerImage oldDockerImage, CancellationToken cancellationToken = default);

        Task<bool> SaveChangesAsync();
    }
}
