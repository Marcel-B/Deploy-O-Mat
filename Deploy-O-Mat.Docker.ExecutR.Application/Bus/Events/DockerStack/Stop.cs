using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerStack
{
    public class Stop
    {
        public class StopDockerStackEvent : Event
        {
            public string Name { get; set; }
            public string File { get; set; }

            public StopDockerStackEvent(
                string name,
                string file)
            {
                Name = name;
                File = file;
            }
        }

        public class DockerStackEventEventHandler : IEventHandler<Stop.StopDockerStackEvent>
        {
            private readonly ILogger<Stop.StopDockerStackEvent> _logger;
            private readonly IDockerStackService _dockerStackService;

            public DockerStackEventEventHandler(
                IDockerStackService dockerStackService,
                ILogger<Stop.StopDockerStackEvent> logger)
            {
                _dockerStackService = dockerStackService;
                _logger = logger;
            }

            public async Task Handle(
                Stop.StopDockerStackEvent @event)
            {
                _logger.LogInformation($"Try stop stack '{@event.Name}'");
                _ = await _dockerStackService.StopStack(@event.Name);
            }
        }
    }
}