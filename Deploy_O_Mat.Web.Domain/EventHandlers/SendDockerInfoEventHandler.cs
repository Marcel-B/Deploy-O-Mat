using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MicroRabbit.Domain.Core.Bus;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.EventHandlers
{
    public class SendDockerInfoEventHandler : IEventHandler<SendDockerInfoEvent>
    {
        public SendDockerInfoEventHandler()
        {
        }

        public Task Handle(SendDockerInfoEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
