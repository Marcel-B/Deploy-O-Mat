using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers
{
    public class CreateDockerServiceCommandHandler : IRequestHandler<CreateCreateDockerServiceCommand, bool>
    {
        private IEventBus _eventBus;

        public CreateDockerServiceCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateCreateDockerServiceCommand request,
            CancellationToken cancellationToken)
        {
            _eventBus.Publish(new DockerServiceCreatedEvent(request.Name, request.Repo, request.Tag, request.Network, request.Script));
            return Task.FromResult(true);
        }
    }
}
