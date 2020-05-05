using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers
{
    public class UpdateServicesEventHandler : IEventHandler<UpdateServicesEvent>
    {
        public UpdateServicesEventHandler()
        {
            
        }
        public Task Handle(UpdateServicesEvent @event)
        {
            var e = @event.DockerServices;
            foreach (var dockerService in e)
            {
                Console.WriteLine(dockerService.Name);
            }
            return Task.CompletedTask;
        }
    }
}