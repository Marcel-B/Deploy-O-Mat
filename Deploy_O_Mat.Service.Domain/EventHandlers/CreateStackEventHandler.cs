using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class CreateStackEventHandler : IEventHandler<StackCreatedEvent>
    {
        private ILogger<CreateStackEventHandler> _logger;
        private IDockerStackService _dockerStackService;
        private readonly IDockerInfoService dockerInfoService;
        private readonly IEventBus eventBus;

        public CreateStackEventHandler(
            IDockerStackService dockerStackService,
            IDockerInfoService dockerInfoService,
            IEventBus eventBus,
            ILogger<CreateStackEventHandler> logger)
        {
            _dockerStackService = dockerStackService;
            this.dockerInfoService = dockerInfoService;
            this.eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(
            StackCreatedEvent @event)
        {

            _logger.LogInformation($"Try create stack {@event.Name} Path {@event.File}");
            await _dockerStackService.CreateStack(new Models.DockerStack
            {
                File = @event.File,
                Name = @event.Name,
            });
            string services = "";

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
            _logger.LogInformation($"Create Stack");
        }
    }
}

