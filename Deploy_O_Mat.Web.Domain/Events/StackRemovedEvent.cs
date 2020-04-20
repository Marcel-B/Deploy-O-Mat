using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
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
