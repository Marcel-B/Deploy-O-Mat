using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Events;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.EventHandlers
{
    public class SendDockerInfoEventHandler : IEventHandler<SendDockerInfoEvent>
    {
        private readonly ILogger<SendDockerInfoEventHandler> _logger;

        public SendDockerInfoEventHandler(
            ILogger<SendDockerInfoEventHandler> logger)
        {
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
            var sb = new StringBuilder();
            foreach (var value in values.Skip(1))
            {
                if (idIdx > value.Length)
                    continue;
                var id = value.Substring(0, idIdx);
                var name = value[idIdx..nameIdx];
                var mode = value[nameIdx..modeIdx];
                var replicas = value[modeIdx..replicasIdx];
                var image = value[replicasIdx..imageIdx];
                var ports = value[imageIdx..];
                sb.AppendLine($"'{id.Trim()}'");
                sb.AppendLine($"'{name.Trim()}'");
                sb.AppendLine($"'{mode.Trim()}'");
                sb.AppendLine($"'{replicas.Trim()}'");
                sb.AppendLine($"'{image.Trim()}'");
                sb.AppendLine($"'{ports.Trim()}'");
            }
            _logger.LogError(sb.ToString());
            return Task.CompletedTask;
        }
    }
}
