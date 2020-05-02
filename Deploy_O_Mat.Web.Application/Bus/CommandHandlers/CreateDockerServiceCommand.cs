using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
        public class CreateDockerServiceCommand : Command
        {
            public string Name { get; set; }
            public string Repo { get; set; }
            public string Tag { get; set; }
            public string Network { get; set; }
            public string Script { get; set; }
        }
        public class CreateCreateDockerServiceCommand : CreateDockerServiceCommand
        {
            public CreateCreateDockerServiceCommand(
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
        
        public class CreateDockerServiceCommandHandler : IRequestHandler<CreateCreateDockerServiceCommand, bool>
        {
            private readonly IEventBus _eventBus;

            public CreateDockerServiceCommandHandler(
                IEventBus eventBus)
            {
                _eventBus = eventBus;
            }

            public Task<bool> Handle(
                CreateCreateDockerServiceCommand request,
                CancellationToken cancellationToken)
            {
                _eventBus.Publish(new DockerServiceCreatedEvent(request.Name, request.Repo, request.Tag, request.Network, request.Script));
                return Task.FromResult(true);
            }
        }
    }
