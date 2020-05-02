using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events
{
    public class SendDockerInfoEvent : Event
    {
        public string DockerInfo { get; set; }
    }
}
