using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
    public class RemoveStackCommand : Command
    {
        public string StackName { get; set; }
    }
    public class CreateRemoveStackCommand : RemoveStackCommand
    {
        public CreateRemoveStackCommand(
            string stackName)
        {
            StackName = stackName;
        }
    }
    public class RemoveStackCommandHandler : IRequestHandler<CreateRemoveStackCommand, bool>
    {
        private IEventBus _eventBus;
    
        public RemoveStackCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    
        public Task<bool> Handle(
            CreateRemoveStackCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new DockerStackRemovedEvent(request.StackName));
            return Task.FromResult(true);
        }
    }
}