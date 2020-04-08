using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Commands;
using com.b_velop.Deploy_O_Mat.Application.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Application.CommandHandler
{
    public class ServiceUpdateCommandHandler : IRequestHandler<ServiceUpdateCommand, bool>
    {
        private IEventBus _eventBus;

        public ServiceUpdateCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(ServiceUpdateCommand request, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new ServiceUpdateEvent(request.ServiceName, request.RepoName, request.Tag, request.BuildId));
            return Task.FromResult(true);
        }
    }
}
