using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Domain.CommandHandlers
{
    public class ServiceUpdateCommandHandler : IRequestHandler<ServiceUpdateCommand, bool>
    {
        private IEventBus _eventBus;

        public ServiceUpdateCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            ServiceUpdateCommand request,
            CancellationToken cancellationToken)
        {
            // Publish event to RabbitMQ
            _eventBus.Publish(new ServiceUpdatedEvent(request.ServiceName, request.RepoName, request.Tag, request.BuildId));
            return Task.FromResult(true);
        }
    }
}
