using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
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
}
