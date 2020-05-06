using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
    public class CreateStackCommand : Command
    {
        public string File { get; set; }
        public string Name { get; set; }
    }
    public class CreateCreateStackCommand : CreateStackCommand
    {
        public CreateCreateStackCommand(
            string name,
            string file)
        {
            File = file;
            Name = name;
        }
    }
    public class CreateStackCommandHandler : IRequestHandler<CreateCreateStackCommand, bool>
    {
        private readonly IEventBus _eventBus;

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
            _eventBus.Publish(new DockerStackCreatedEvent(request.File, request.Name));
            return Task.FromResult(true);
        }
    }
}