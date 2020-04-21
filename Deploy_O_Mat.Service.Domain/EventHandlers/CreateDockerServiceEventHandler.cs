using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class CreateDockerServiceEventHandler : IEventHandler<DockerServiceCreatedEvent>
    {
        private IEventBus _eventBus;
        private IDockerServiceService _service;
        private IDockerInfoService _dockerInfoService;

        public CreateDockerServiceEventHandler(
            IEventBus eventBus,
            IDockerInfoService dockerInfoService,
            IDockerServiceService service)
        {
            _eventBus = eventBus;
            _service = service;
            _dockerInfoService = dockerInfoService;
        }

        public async Task Handle(
            DockerServiceCreatedEvent @event)
        {
            await _service.Create(new DockerService
            {
                Name = @event.Name,
                RepoName = @event.Repo,
                Tag = @event.Tag,
                Script = @event.Script,
                Network = @event.Network
            });

            var services = string.Empty;
#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await _dockerInfoService.GetServices();
#endif

            await _eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
        }
    }
}
