using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class UpdateDockerServiceEventHandler : IEventHandler<DockerServiceUpdatedEvent>
    {
        private ILogger<UpdateDockerServiceEventHandler> _logger;
        private IDockerServiceService _dockerService;
        private readonly IDockerInfoService dockerInfoService;
        private readonly IEventBus eventBus;

        public UpdateDockerServiceEventHandler(
            IDockerServiceService dockerService,
            IDockerInfoService dockerInfoService,
            IEventBus eventBus,
            ILogger<UpdateDockerServiceEventHandler> logger)
        {
            _dockerService = dockerService;
            this.dockerInfoService = dockerInfoService;
            this.eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(
            DockerServiceUpdatedEvent @event)
        {
            _logger.LogInformation($"Try update {@event.Image} {@event.Service}");
            var result = await _dockerService.UpdateService(@event.Image, @event.Service);
            string services = "";

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
            _logger.LogInformation($"Update {@event.Service} {@event.Image} result id = {result}");
        }
    }
}
