using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
{
    public class DockerStackCreatedEvent : Event
    {
        public string File { get; set; }
        public string Name { get; set; }

        public DockerStackCreatedEvent(
            string file,
            string name)
        {
            File = file;
            Name = name;
        }
    }
}

