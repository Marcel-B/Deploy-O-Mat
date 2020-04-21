using System;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class CreateDockerServiceEventHandler : IEventHandler<DockerServiceCreatedEvent>
    {
        private IDockerServiceService _service;

        public CreateDockerServiceEventHandler(
            IDockerServiceService service)
        {
            _service = service;
        }

        public Task Handle(
            DockerServiceCreatedEvent @event)
        {
            _service.Create(new DockerService
            {
                Name = @event.Name,
                RepoName = @event.Repo,
                Tag = @event.Tag,
                Script = @event.Script,
                Network = @event.Network
            });
            return Task.CompletedTask;
        }
    }
}
