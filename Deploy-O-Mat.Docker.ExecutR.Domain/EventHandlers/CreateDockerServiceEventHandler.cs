using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers
{
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
            await _service.Create(new DockerService
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
