using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Contracts
{
    public interface IDockerStackService
    {
        Task StartStack(Guid id, CancellationToken cancellationToken = default);
        Task StopStack(Guid id, CancellationToken cancellationToken = default);
        Task CreateStack(Guid id, CancellationToken cancellationToken = default);
        Task RemoveStack(Guid id, CancellationToken cancellationToken = default);
        Task<IServiceResponse> UpdateStack(Guid id, string serviceName, CancellationToken cancellationToken = default);
    }
}
