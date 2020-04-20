using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class RemoveServiceCommandHandler : IRequestHandler<CreateRemoveServiceCommand, bool>
    {
        private IEventBus _eventBus;

        public RemoveServiceCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateRemoveServiceCommand request,
            CancellationToken cancellationToken)
        {
            _eventBus.Publish(new ServiceRemovedEvent(request.ServiceName));
            return Task.FromResult(true);
        }
    }
}
