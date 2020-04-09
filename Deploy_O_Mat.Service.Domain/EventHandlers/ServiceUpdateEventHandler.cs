using System;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Events;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class ServiceUpdateEventHandler : IEventHandler<ServiceUpdateEvent>
    {
        private ILogger<ServiceUpdateEventHandler> _logger;

        public ServiceUpdateEventHandler()
        {
            //_logger = logger;
        }

        public Task Handle(
            ServiceUpdateEvent @event)
        {
            
           Console.WriteLine($"Try update {@event.RepoName} {@event.BuildId}");
            return Task.CompletedTask;
        }
    }
}
