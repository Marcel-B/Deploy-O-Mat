using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class CreateStackCommandHandler : IRequestHandler<CreateCreateStackCommand, bool>
    {
        private IEventBus _eventBus;

        public CreateStackCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateCreateStackCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new StackCreatedEvent(request.File, request.Name));
            return Task.FromResult(true);
        }
    }
}