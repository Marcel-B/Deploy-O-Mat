using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class UpdateServiceCommandHandler : IRequestHandler<CreateUpdateServiceCommand, bool>
    {
        private IEventBus _eventBus;

        public UpdateServiceCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateUpdateServiceCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new ServiceUpdatedEvent(request.ServiceName, request.RepoName, request.Tag, request.BuildId));
            return Task.FromResult(true);
        }
    }
}
