using System;
using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
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
