using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService
{
    public class Update
    {
        public class DockerServiceUpdatedEvent : Event
        {
            public string Image { get; set; }
            public string Service { get; set; }

            public DockerServiceUpdatedEvent(
                string image,
                string service)
            {
                Service = service;
                Image = image;
            }
        }
        
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
}