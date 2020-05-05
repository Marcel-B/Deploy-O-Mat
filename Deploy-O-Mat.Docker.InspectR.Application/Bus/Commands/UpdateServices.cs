using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Events;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands
{
    public class UpdateServices : Command
    {
        public IEnumerable<DockerService> DockerServices { get; set; }
    }

    public class CreateUpdateServices : UpdateServices
    {
        public CreateUpdateServices(
            IEnumerable<DockerService> dockerServices)
        {
            DockerServices = dockerServices;
        }
    }

    public class UpdateServicesCommandHandler : IRequestHandler<CreateUpdateServices, bool>
    {
        private readonly IEventBus _eventBus;

        public UpdateServicesCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public Task<bool> Handle(CreateUpdateServices request, CancellationToken cancellationToken)
        {
            _eventBus.Publish(new UpdateServicesEvent(request.DockerServices));
            return Task.FromResult(true);
        }
    }
}