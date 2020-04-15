using System.Threading;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;

namespace Deploy_O_Mat.Service.Domain.CommandHandlers
{
    public class SendDockerInfoCommandHandler : IRequestHandler<CreateSendDockerInfoCommand, bool>
    {
        private readonly IEventBus _eventBus;

        public SendDockerInfoCommandHandler(
            IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task<bool> Handle(
            CreateSendDockerInfoCommand request,
            CancellationToken cancellationToken)
        {
            _eventBus.Publish(new SendDockerInfoEvent(request.DockerInfo));
            return Task.FromResult(true);
        }
    }
}
