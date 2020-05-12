using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Events.ServiceDetail
{
    public class Details
    {
        public class DockerServiceDetailEvent : Event
        {
            public string Name { get; set; }
            public string Repo { get; set; }
            public string Tag { get; set; }
            public string Network { get; set; }
            public string Script { get; set; }

            public DockerServiceDetailEvent()
            {
                // Name = name;
                // Repo = repo;
                // Tag = tag;
                // Network = network;
                // Script = script;
            }
        }

        public class DockerServiceDetailEventHandler : IEventHandler<DockerServiceDetailEvent>
        {
            private readonly IDockerServiceService _service;
            private readonly ILogger<DockerServiceDetailEventHandler> _logger;

            public DockerServiceDetailEventHandler(
                IDockerServiceService service,
                ILogger<DockerServiceDetailEventHandler> logger)
            {
                _service = service;
                _logger = logger;
            }

            public async Task Handle(
                DockerServiceDetailEvent @event)
            {
                try
                {

                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error while requesting DockerDetails");
                }
                // await _service.Create(new Domain.Models.DockerService
                // {
                //     Name = @event.Name,
                //     RepoName = @event.Repo,
                //     Tag = @event.Tag,
                //     Script = @event.Script,
                //     Network = @event.Network
                // });
            }
        }
    }
}