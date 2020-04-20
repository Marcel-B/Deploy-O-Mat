using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class StackRemovedEvent : Event
    {
        public string StackName { get; set; }

        public StackRemovedEvent(
            string stackName)
        {
            StackName = stackName;
        }
    }
}
