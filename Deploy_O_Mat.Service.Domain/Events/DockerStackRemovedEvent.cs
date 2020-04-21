using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class DockerStackRemovedEvent : Event
    {
        public string StackName { get; set; }

        public DockerStackRemovedEvent(
            string stackName)
        {
            StackName = stackName;
        }
    }
}
