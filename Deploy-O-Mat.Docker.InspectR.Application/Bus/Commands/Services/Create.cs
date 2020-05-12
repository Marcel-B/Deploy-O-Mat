using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using MediatR;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands.Services
{
    public class Create
    {
        public class UpdateServices : Command
        {
            public IEnumerable<DockerService> DockerServices { get; set; }
        }
        
        public class UpdateServicesEvent : Event
        {
            public IEnumerable<DockerService> DockerServices { get; set; }
        
            public UpdateServicesEvent(
                IEnumerable<DockerService> dockerServices)
            {
                DockerServices = dockerServices;
            }
        }
        
        public class UpdateServicesCommandHandler : IRequestHandler<UpdateServices, bool>
        {
            private readonly IEventBus _eventBus;

            public UpdateServicesCommandHandler(IEventBus eventBus)
            {
                _eventBus = eventBus;
            }

            public Task<bool> Handle(
                UpdateServices request,
                CancellationToken cancellationToken)
            {
                _eventBus.Publish(new UpdateServicesEvent(request.DockerServices));
                return Task.FromResult(true);
            }
        }
    }
}