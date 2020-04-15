using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
{
    public class StackCreatedEvent : Event
    {
        public string File { get; set; }
        public string Name { get; set; }

        public StackCreatedEvent(
            string file,
            string name)
        {
            File = file;
            Name = name;
        }
    }
}

