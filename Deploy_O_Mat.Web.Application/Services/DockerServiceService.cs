using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private IEventBus _eventBus;
        private IDeployOMatWebRepository _repo;

        public DockerServiceService(
            IEventBus eventBus,
            IDeployOMatWebRepository repo)
        {
            _eventBus = eventBus;
            _repo = repo;
        }

        public async Task CreateDockerService(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerService = await _repo.GetDockerService(id);

            if (dockerService == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"DockerService with id '{id}' not found");

            await _eventBus.SendCommand(new CreateCreateDockerServiceCommand(dockerService.Name, dockerService.Repo, dockerService.Tag, dockerService.Network, dockerService.Script));
        }

        public async Task<IEnumerable<Domain.Models.DockerService>> Get(
            CancellationToken cancellationToken = default)
            => await _repo.GetDockerServices();

        public async Task RemoveDockerService(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerService = await _repo.GetDockerService(id);

            if (dockerService == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound, $"DockerService with id '{id}' not found");

            await _eventBus.SendCommand(new CreateRemoveDockerServiceCommand(dockerService.Name));
        }
    }
}
