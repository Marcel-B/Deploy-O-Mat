using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.SignalR;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerStackLogService : IDockerStackLogService
    {
        private readonly IHubContext<DockerServiceUpdateHub> _hub;
        private readonly IDeployOMatWebRepository _repository;
        private readonly ILogger<DockerStackLogService> _logger;

        public DockerStackLogService(
            IHubContext<DockerServiceUpdateHub> hub,
            IDeployOMatWebRepository repository,
            ILogger<DockerStackLogService> logger)
        {
            _hub = hub;
            _repository = repository;
            _logger = logger;
        }
        public async Task UpdateStackLogs(IEnumerable<Domain.Models.DockerStackLog> dockerStackLogs)
        { 
            _repository.CreateOrUpdateDockerStackLog(dockerStackLogs).Wait();
            var stackLogs = _repository.GetDockerStackLogs();
            var s = new SocketDto();
            foreach (var stackLog in stackLogs)
            {
                s.Values.Add(new TransferData{
                    Id = stackLog.Id.ToString(),
                    Image = stackLog.Image,
                    Service = stackLog.Name,
                    IsActive = stackLog.IsActive,
                    Replicas = $"{stackLog.ReplicasOnline}/{stackLog.Replicas}"
                });
            }
            var v = JsonSerializer.Serialize(s);
            _logger.LogInformation($"Update information with\n{v}");
            await _hub.Clients.All.SendAsync("SendUpdate", v);
        }
    }
}