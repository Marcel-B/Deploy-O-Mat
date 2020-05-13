using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerStack
{
    public class Start
    {
        public class StartDockerStackEvent : Event
        {
            public string Name { get; set; }
            public string File { get; set; }

            public StartDockerStackEvent(
                string name,
                string file)
            {
                Name = name;
                File = file;
            }
        }
        
        public class DockerStackEventEventHandler : IEventHandler<Start.StartDockerStackEvent>
        {
            private readonly ILogger<Start.StartDockerStackEvent> _logger;
            private readonly IDockerStackService _dockerStackService;

            public DockerStackEventEventHandler(
                IDockerStackService dockerStackService,
                ILogger<Start.StartDockerStackEvent> logger)
            {
                _dockerStackService = dockerStackService;
                _logger = logger;
            }

            public async Task Handle(
                Start.StartDockerStackEvent @event)
            {
                _logger.LogInformation($"Try start stack '{@event.Name}' Path '{@event.File}'");
                _ = await _dockerStackService.StartStack(new Domain.Models.DockerStack
                {
                    File = @event.File,
                    Name = @event.Name,
                });
            }
        }
    }
}