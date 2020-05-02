using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers
{
    public abstract class UpdateDockerServiceCommand : Command
    {
        public string Service { get; set; }
        public string Image { get; set; }
    }
    
    public class CreateUpdateDockerServiceCommand : UpdateDockerServiceCommand
    {
        public CreateUpdateDockerServiceCommand(
            string image,
            string service)
        {
            Service = service;
            Image = image;
        }
    }
    
    public class UpdateDockerServiceCommandHandler : IRequestHandler<CreateUpdateDockerServiceCommand, bool>
    {
        private readonly IEventBus _eventBus;
    
        public UpdateDockerServiceCommandHandler(
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