using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers
{
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
            _ = await _dockerStackService.CreateStack(new DockerStack
            {
                File = @event.File,
                Name = @event.Name,
            });
        }
    }
}
