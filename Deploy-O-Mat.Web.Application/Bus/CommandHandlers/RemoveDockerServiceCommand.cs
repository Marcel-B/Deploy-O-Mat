using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
        public class RemoveDockerServiceCommand : Command
        {
            public string ServiceName { get; set; }
        }
        
        public class CreateRemoveDockerServiceCommand : RemoveDockerServiceCommand
        {
            public CreateRemoveDockerServiceCommand(
                string serviceName)
            {
                ServiceName = serviceName;
            }
        }
        
        public class RemoveDockerServiceCommandHandler: IRequestHandler<CreateRemoveDockerServiceCommand, bool>
        {
            private readonly IEventBus _eventBus;
        
            public RemoveDockerServiceCommandHandler(
                IEventBus eventBus)
            {
                _eventBus = eventBus;
            }
        
            public Task<bool> Handle(
                CreateRemoveDockerServiceCommand request,
                CancellationToken cancellationToken)
            {
                _eventBus.Publish(new DockerServiceRemovedEvent(request.ServiceName));
                return Task.FromResult(true);
            }
        }
}