using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService
{
    public class Create
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

        public class CreateDockerServiceEventHandler : IEventHandler<DockerServiceCreatedEvent>
        {
            private readonly IDockerServiceService _service;

            public CreateDockerServiceEventHandler(
                IDockerServiceService service)
            {
                _service = service;
            }

            public async Task Handle(
                DockerServiceCreatedEvent @event)
            {
                await _service.Create(new Domain.Models.DockerService
                {
                    Name = @event.Name,
                    RepoName = @event.Repo,
                    Tag = @event.Tag,
                    Script = @event.Script,
                    Network = @event.Network
                });
            }
        }
    }
}