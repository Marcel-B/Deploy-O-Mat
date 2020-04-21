using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly IEventBus _eventBus;
        private readonly IDeployOMatWebRepository _repo;

        public DockerStackService(
            IEventBus eventBus,
            IDeployOMatWebRepository repo)
        {
            _eventBus = eventBus;
            _repo = repo;
        }

        public async Task CreateStack(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);

            if (dockerStack == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { dockerStack = "Not found", id });

            await _eventBus.SendCommand(new CreateCreateStackCommand(dockerStack.Name, dockerStack.File));
        }

        public async Task RemoveStack(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerStack = await _repo.GetDockerStack(id);

            if (dockerStack == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, new { dockerStack = "Not found", id });

            await _eventBus.SendCommand(new CreateRemoveStackCommand(dockerStack.Name));
        }
    }
}
