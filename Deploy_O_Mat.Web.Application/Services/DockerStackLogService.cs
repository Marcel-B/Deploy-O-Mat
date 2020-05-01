using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.SignalR;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class DockerStackLogService : IDockerStackLogService
    {
        private readonly IHubContext<DockerServiceUpdateHub> _hub;
        private readonly IDeployOMatWebRepository _repository;

        public DockerStackLogService(
            IHubContext<DockerServiceUpdateHub> hub,
            IDeployOMatWebRepository repository)
        {
            _hub = hub;
            _repository = repository;
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
            await _hub.Clients.All.SendAsync("SendUpdate", v);
        }
    }
}