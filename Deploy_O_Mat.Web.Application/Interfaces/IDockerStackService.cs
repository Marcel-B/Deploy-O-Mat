using System;
using System.Threading;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IDockerStackService
    {
        Task CreateStack(Guid id, CancellationToken cancellationToken = default);
        Task RemoveStack(Guid id, CancellationToken cancellationToken = default);
        Task<IServiceResponse> UpdateStack(Guid id, string serviceName, CancellationToken cancellationToken = default);
    }
}
