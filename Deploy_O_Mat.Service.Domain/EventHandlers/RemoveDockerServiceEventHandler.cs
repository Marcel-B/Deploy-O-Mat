using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class RemoveDockerServiceEventHandler : IEventHandler<DockerServiceRemovedEvent>
    {
        private readonly IEventBus eventBus;
        private readonly IDockerInfoService dockerInfoService;
        private IDockerServiceService _dockerServiceService;

        public RemoveDockerServiceEventHandler(
            IEventBus eventBus,
            IDockerInfoService dockerInfoService,
            IDockerServiceService dockerServiceService)
        {
            this.eventBus = eventBus;
            this.dockerInfoService = dockerInfoService;
            _dockerServiceService = dockerServiceService;
        }

        public async Task Handle(
            DockerServiceRemovedEvent @event)
        {
            await _dockerServiceService.Remove(@event.ServiceName);
            string services = "";

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
        }
    }
}
