using System;
using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
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
