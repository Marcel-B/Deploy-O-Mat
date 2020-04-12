using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class ServiceUpdateEventHandler : IEventHandler<ServiceUpdatedEvent>
    {
        private ILogger<ServiceUpdateEventHandler> _logger;
        private IDockerServiceService _dockerService;
        private IDockerServiceRepository _dockerServiceRepository;

        public ServiceUpdateEventHandler(
            IDockerServiceService dockerService,
            IDockerServiceRepository dockerServiceRepository,
            ILogger<ServiceUpdateEventHandler> logger)
        {
            _dockerService = dockerService;
            _dockerServiceRepository = dockerServiceRepository;
            _logger = logger;
        }

        public async Task Handle(
            ServiceUpdatedEvent @event)
        {

            _logger.LogInformation($"Try update {@event.RepoName} {@event.BuildId}");
            var result = await _dockerService.UpdateService(new Models.DockerService
            {
                BuildId = @event.BuildId,
                RepoName = @event.RepoName,
                Name = @event.ServiceName,
                Tag = @event.Tag,
            });
            _logger.LogInformation($"Update {@event.RepoName} {@event.BuildId} result id = {result}");
        }
    }
}
