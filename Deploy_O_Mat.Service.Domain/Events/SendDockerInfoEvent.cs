using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class SendDockerInfoEvent : Event
    {
        public string DockerInfo { get; set; }

        public SendDockerInfoEvent(
            string dockerInfo)
        {
            DockerInfo = dockerInfo;
        }
    }
}
