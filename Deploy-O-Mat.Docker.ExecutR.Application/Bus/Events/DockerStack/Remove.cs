using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerStack
{
    public class Remove
    {
        public class DockerStackRemovedEvent : Event
        {
            public string StackName { get; set; }

            public DockerStackRemovedEvent(
                string stackName)
            {
                StackName = stackName;
            }
        }
        
        public class RemoveDockerStackEventHandler : IEventHandler<DockerStackRemovedEvent>
        {
            private readonly IDockerStackService _dockerStackService;
            private readonly ILogger<RemoveDockerStackEventHandler> _logger;

            public RemoveDockerStackEventHandler(
                IDockerStackService dockerStackService,
                ILogger<RemoveDockerStackEventHandler> logger)
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
}