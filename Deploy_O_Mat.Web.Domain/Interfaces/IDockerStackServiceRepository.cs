using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces
{
    public interface IDockerStackServiceRepository
    {
        Task<IEnumerable<DockerStackService>> GetAll(CancellationToken cancellationToken = default);
        Task<DockerStackService> Get(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync();
    }
}
