using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Commands.DockerStack
{
    public class Start
    {
        public class DockerStack : Command
        {
            public string Name { get; set; }
            public string File { get; set; }
        }

        public class StartDockerStackEvent : Event
        {
            public string Name { get; set; }
            public string File { get; set; }

            public StartDockerStackEvent(
                string name,
                string file)
            {
                Name = name;
                File = file;
            }
        }

        public class DockerStackCommandHandler : IRequestHandler<Start.DockerStack, bool>
        {
            private readonly IEventBus _eventBus;

            public DockerStackCommandHandler(IEventBus eventBus)
            {
                _eventBus = eventBus;
            }

            public Task<bool> Handle(Start.DockerStack request, CancellationToken cancellationToken)
            {
                _eventBus.Publish(new StartDockerStackEvent(request.Name, request.File));
                return Task.FromResult(true);
            }
        }
    }
}