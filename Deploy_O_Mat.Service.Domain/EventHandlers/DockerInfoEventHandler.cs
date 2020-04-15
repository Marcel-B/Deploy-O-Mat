using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class DockerInfoEventHandler : IEventHandler<DockerInfoEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IDockerInfoService _dockerInfoService;

        public DockerInfoEventHandler(
            IEventBus eventBus,
            IDockerInfoService dockerInfoService)
        {
            _eventBus = eventBus;
            _dockerInfoService = dockerInfoService;
        }

        public async Task Handle(DockerInfoEvent @event)
        {
            var services = await _dockerInfoService.GetServices();
            var createSendDockerInfoCommand = new CreateSendDockerInfoCommand(
              services);
            await _eventBus.SendCommand(createSendDockerInfoCommand);
        }
    }
}
