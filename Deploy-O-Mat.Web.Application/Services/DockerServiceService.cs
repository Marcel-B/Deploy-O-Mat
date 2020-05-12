using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;

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
                throw new RestException(System.Net.HttpStatusCode.NotFound, new {dockerService = "Not found", id});

            await _eventBus.SendCommand(new CreateCreateDockerServiceCommand(dockerService.Name, dockerService.Repo,
                dockerService.Tag, dockerService.Network, dockerService.Script));
            
            dockerService.LastRestart = DateTime.UtcNow;
            _ = await _repo.SaveChangesAsync(cancellationToken);
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
                throw new RestException(System.Net.HttpStatusCode.NotFound, new {dockerService = "Not found", id});

            await _eventBus.SendCommand(new CreateRemoveDockerServiceCommand(dockerService.Name));
        }

        public async Task<IServiceResponse> UpdateDockerService(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var dockerService = await _repo.GetDockerService(id);

            if (dockerService == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Error = new {dockerService = "Not found", id},
                    Message = $"DockerService not found",
                    HttpStatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            var image = dockerService.Image;
            var service = dockerService.Name;

            await _eventBus.SendCommand(new CreateUpdateDockerServiceCommand(image, service));

            return new ServiceResponse
            {
                Success = true,
            };
        }
    }
}