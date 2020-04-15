using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class UpdateServiceEventHandler : IEventHandler<ServiceUpdatedEvent>
    {
        private ILogger<UpdateServiceEventHandler> _logger;
        private IDockerServiceService _dockerService;
        private IDockerServiceRepository _dockerServiceRepository;

        public UpdateServiceEventHandler(
            IDockerServiceService dockerService,
            IDockerServiceRepository dockerServiceRepository,
            ILogger<UpdateServiceEventHandler> logger)
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
