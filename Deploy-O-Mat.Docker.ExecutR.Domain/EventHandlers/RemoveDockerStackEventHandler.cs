using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers
{
    public class RemoveDockerStackEventHandler : IEventHandler<DockerStackRemovedEvent>
    {
        private readonly IDockerStackService _dockerStackService;
        private readonly ILogger<RemoveDockerServiceEventHandler> _logger;

        public RemoveDockerStackEventHandler(
            IDockerStackService dockerStackService,
            ILogger<RemoveDockerServiceEventHandler> logger)
        {
            _dockerStackService = dockerStackService;
            _logger = logger;
        }

        public async Task Handle(
            DockerStackRemovedEvent @event)
        {
            try
            {
                _ = await _dockerStackService.RemoveStack(@event.StackName);
                _logger.LogInformation(($"DockerStack '{@event.StackName}' removed"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing DockerStack '{@event.StackName}'");
            }
        }
    }
}
