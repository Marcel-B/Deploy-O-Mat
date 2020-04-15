using System;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Domain.EventHandlers
{
    public class CreateStackEventHandler : IEventHandler<StackCreatedEvent>
    {
        private ILogger<CreateStackEventHandler> _logger;
        private IDockerStackService _dockerStackService;

        public CreateStackEventHandler(
            IDockerStackService dockerStackService,
            ILogger<CreateStackEventHandler> logger)
        {
            _dockerStackService = dockerStackService;
            _logger = logger;
        }

        public async Task Handle(
            StackCreatedEvent @event)
        {

            _logger.LogInformation($"Try create stack {@event.Name} Path {@event.File}");
            var result = await _dockerStackService.CreateStack(new Models.DockerStack
            {
                File = @event.File,
                Name = @event.Name,
            });
            _logger.LogInformation($"Create Stack");
        }
    }
}

