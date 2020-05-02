using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers
{
    public class DockerInfoEvent : Event
    {
    }

    public class SendDockerInfoEventHandler : IEventHandler<SendDockerInfoEvent>
    {
        private readonly IDockerStackLogService _stackLogService;
        private readonly ILogger<SendDockerInfoEventHandler> _logger;

        public SendDockerInfoEventHandler(
            IDockerStackLogService stackLogService,
            ILogger<SendDockerInfoEventHandler> logger)
        {
            _stackLogService = stackLogService;
            _logger = logger;
        }

        public Task Handle(
            SendDockerInfoEvent @event)
        {
            //_logger.LogInformation($"{@event.Timestamp} -- {@event.DockerInfo}");
            var values = @event.DockerInfo.Split('\n');
            var idIdx = values.First().IndexOf("NAME");
            var nameIdx = values.First().IndexOf("MODE");
            var modeIdx = values.First().IndexOf("REPLICAS");
            var replicasIdx = values.First().IndexOf("IMAGE");
            var imageIdx = values.First().IndexOf("PORTS");
            //var sb = new StringBuilder();
            var lst = new List<Domain.Models.DockerStackLog>();
            foreach (var value in values.Skip(1))
            {
                if (idIdx > value.Length)
                    continue;
                if (nameIdx < 0 || modeIdx < 0 || replicasIdx < 0 || imageIdx < 0)
                    continue;
                var id = value.Substring(0, idIdx);
                var name = value[idIdx..nameIdx];
                var mode = value[nameIdx..modeIdx];
                var replicas = value[modeIdx..replicasIdx].Split('/');
                var image = "";
                var ports = "";
                if (value.Length > imageIdx)
                {
                    image = value[replicasIdx..imageIdx];
                    ports = value[imageIdx..];
                }
                else
                {
                    image = value[replicasIdx..];
                }

                lst.Add(new Domain.Models.DockerStackLog
                {
                    Image = image.Trim(),
                    Mode = mode.Trim(),
                    Name = name.Trim(),
                    Ports = ports.Trim(),
                    ServiceId = id.Trim(),
                    Updated = DateTime.UtcNow,
                    ReplicasOnline = int.Parse(replicas[0].Trim()),
                    Replicas = int.Parse(replicas[1].Trim()),
                });
            }

            _stackLogService.UpdateStackLogs(lst);
            return Task.CompletedTask;
        }
    }
}