using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers
{
    public class RemoveDockerServiceEventHandler : IEventHandler<DockerServiceRemovedEvent>
    {
        private readonly IDockerServiceService _dockerServiceService;

        public RemoveDockerServiceEventHandler(
            IDockerServiceService dockerServiceService)
        {
            _dockerServiceService = dockerServiceService;
        }

        public async Task Handle(
            DockerServiceRemovedEvent @event)
        {
            _ = await _dockerServiceService.Remove(@event.ServiceName);
        }
    }
}
