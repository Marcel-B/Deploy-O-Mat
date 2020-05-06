using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers
{
    public class UpdateDockerServiceEventHandler : IEventHandler<DockerServiceUpdatedEvent>
    {
        private readonly ILogger<UpdateDockerServiceEventHandler> _logger;
        private readonly IDockerServiceService _dockerService;

        public UpdateDockerServiceEventHandler(
            IDockerServiceService dockerService,
            ILogger<UpdateDockerServiceEventHandler> logger)
        {
            _dockerService = dockerService;
            _logger = logger;
        }

        public async Task Handle(
            DockerServiceUpdatedEvent @event)
        {
            _logger.LogInformation($"Try update {@event.Image} {@event.Service}");
            _ = await _dockerService.UpdateService(@event.Image, @event.Service);
        }
    }
}