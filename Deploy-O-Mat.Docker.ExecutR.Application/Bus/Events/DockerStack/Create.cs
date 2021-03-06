using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerStack
{
    public class Create
    {
        public class DockerStackCreatedEvent : Event
        {
            public string File { get; set; }
            public string Name { get; set; }

            public DockerStackCreatedEvent(
                string file,
                string name)
            {
                File = file;
                Name = name;
            }
        }

        public class CreateDockerStackEventHandler : IEventHandler<DockerStackCreatedEvent>
        {
            private readonly ILogger<CreateDockerStackEventHandler> _logger;
            private readonly IDockerStackService _dockerStackService;

            public CreateDockerStackEventHandler(
                IDockerStackService dockerStackService,
                ILogger<CreateDockerStackEventHandler> logger)
            {
                _dockerStackService = dockerStackService;
                _logger = logger;
            }

            public async Task Handle(
                DockerStackCreatedEvent @event)
            {
                _logger.LogInformation($"Try create stack '{@event.Name}' Path '{@event.File}'");
                _ = await _dockerStackService.CreateStack(new Domain.Models.DockerStack
                {
                    File = @event.File,
                    Name = @event.Name,
                });
            }
        }
    }
}