using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces
{
    public interface IDockerImageRepository
    {
        DockerImage Create(DockerImage entity, CancellationToken cancellationToken = default, bool saveChanges = true);
        DockerImage Update(DockerImage newDockerImage, DockerImage oldDockerImage, CancellationToken cancellationToken = default);
        Task<IEnumerable<DockerImage>> GetAll(CancellationToken cancellationToken = default);
        Task<DockerImage> Get(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync();
    }
}
