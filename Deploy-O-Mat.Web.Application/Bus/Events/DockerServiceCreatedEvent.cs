using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events
{
    public class DockerServiceCreatedEvent : Event
    {
        public string Name { get; set; }
        public string Repo { get; set; }
        public string Tag { get; set; }
        public string Network { get; set; }
        public string Script { get; set; }

        public DockerServiceCreatedEvent(
            string name,
            string repo,
            string tag,
            string network,
            string script)
        {
            Name = name;
            Repo = repo;
            Tag = tag;
            Network = network;
            Script = script;
        }
    }
}