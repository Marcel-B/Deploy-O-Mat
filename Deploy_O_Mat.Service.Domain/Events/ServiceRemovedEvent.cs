using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class ServiceRemovedEvent : Event
    {
        public string ServiceName { get; set; }
        public ServiceRemovedEvent(
            string serviceName)
        {
            ServiceName = serviceName;
        }
    }
}
