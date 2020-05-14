using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers
{
    public class UpdateServicesEventHandler : IEventHandler<UpdateServicesEvent>
    {
        private readonly IDockerStackLogService _dockerStackLogService;
        private readonly ILogger<UpdateServicesEventHandler> _logger;

        public UpdateServicesEventHandler(
            IDockerStackLogService dockerStackLogService,
            ILogger<UpdateServicesEventHandler> logger)
        {
            _dockerStackLogService = dockerStackLogService;
            _logger = logger;
        }

        public Task Handle(UpdateServicesEvent @event)
        {
            _logger.LogDebug($"Incoming DockerServices information");
            var e = @event.DockerServices;

            var lst = new List<Domain.Models.DockerStackLog>();
            
            foreach (var dockerService in e)
            {
                lst.Add(new Domain.Models.DockerStackLog
                {
                    Image = $"{dockerService.Image}:{dockerService.Tag}",
                    Mode = dockerService.Mode,
                    Name = dockerService.Name,
                    Ports = dockerService.Port,
                    ServiceId = dockerService.ServiceId,
                    Updated = dockerService.Updated,
                    ReplicasOnline = dockerService.ReplicasActive,
                    Replicas = dockerService.Replicas,
                });
            }

            _dockerStackLogService.UpdateStackLogs(lst);
            return Task.CompletedTask;
        }
    }
}