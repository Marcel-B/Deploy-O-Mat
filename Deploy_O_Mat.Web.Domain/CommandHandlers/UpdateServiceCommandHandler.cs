using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class UpdateServiceCommandHandler : IRequestHandler<CreateUpdateDockerServiceCommand, bool>
    {
        private IEventBus _eventBus;

        public UpdateServiceCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateUpdateDockerServiceCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new DockerServiceUpdatedEvent(request.Image, request.Service));
            return Task.FromResult(true);
        }
    }
}
