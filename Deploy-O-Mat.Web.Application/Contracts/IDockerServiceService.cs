using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Contracts
{
    public interface IDockerServiceService
    {
        Task RemoveDockerService(Guid id, CancellationToken cancellationToken = default);
        Task CreateDockerService(Guid id, CancellationToken cancellationToken = default);
        Task<IServiceResponse> UpdateDockerService(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Domain.Models.DockerService>> Get(CancellationToken cancellationToken = default);
    }
}
