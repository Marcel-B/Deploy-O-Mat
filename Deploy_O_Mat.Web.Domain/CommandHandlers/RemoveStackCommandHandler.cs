using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
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
            _eventBus.Publish(new StackRemovedEvent(request.StackName));
            return Task.FromResult(true);
        }
    }
}
