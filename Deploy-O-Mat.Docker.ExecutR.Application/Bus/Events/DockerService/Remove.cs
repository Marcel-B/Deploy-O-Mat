using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService
{
    public class Remove
    {
        public class DockerServiceRemovedEvent : Event
        {
            public string ServiceName { get; set; }

            public DockerServiceRemovedEvent(
                string serviceName)
            {
                ServiceName = serviceName;
            }
        }
        
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
}