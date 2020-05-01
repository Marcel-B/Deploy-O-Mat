using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
    public class DockerInfoCommand : Command
    {
    }
    
    public class DockerInfoCommandHandler : IRequestHandler<DockerInfoCommand, bool>
    {
        private IEventBus _eventBus;
    
        public DockerInfoCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    
        public Task<bool> Handle(
            DockerInfoCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new DockerInfoEvent());
            return Task.FromResult(true);
        }
    }
}