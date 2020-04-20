using System.IO;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class RemoveStackEventHandler : IEventHandler<StackRemovedEvent>
    {
        private readonly IDockerInfoService dockerInfoService;
        private IDockerStackService _dockerStackService;
        private readonly IEventBus eventBus;

        public RemoveStackEventHandler(
            IEventBus eventBus,
            IDockerInfoService dockerInfoService,
            IDockerStackService dockerStackService)
        {
            this.eventBus = eventBus;
            this.dockerInfoService = dockerInfoService;
            _dockerStackService = dockerStackService;
        }


        public async Task Handle(
            StackRemovedEvent @event)
        {
            await _dockerStackService.RemoveStack(@event.StackName);
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
