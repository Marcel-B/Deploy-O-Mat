using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class RemoveDockerServiceCommandHandler: IRequestHandler<CreateRemoveDockerServiceCommand, bool>
    {
        private IEventBus _eventBus;

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
