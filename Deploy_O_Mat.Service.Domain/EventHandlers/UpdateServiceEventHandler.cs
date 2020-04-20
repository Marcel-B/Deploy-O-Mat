using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
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
        private readonly IDockerInfoService dockerInfoService;
        private readonly IEventBus eventBus;

        public UpdateServiceEventHandler(
            IDockerServiceService dockerService,
            IDockerInfoService dockerInfoService,
            IEventBus eventBus,
            ILogger<UpdateServiceEventHandler> logger)
        {
            _dockerService = dockerService;
            this.dockerInfoService = dockerInfoService;
            this.eventBus = eventBus;
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
            string services = "";

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
            _logger.LogInformation($"Update {@event.RepoName} {@event.BuildId} result id = {result}");
        }
    }
}
