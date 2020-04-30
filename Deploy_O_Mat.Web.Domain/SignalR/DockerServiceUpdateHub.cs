using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.SignalR
{
    public interface IDockerServiceUpdate
    {
        Task SendUpdate(DockerStackLog log);
    }

    public class DockerServiceUpdateHub : Hub
    {
        private readonly IMediator _mediator;

        public DockerServiceUpdateHub(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendUpdate(
            IEnumerable<DockerStackLog> log)
        {
            await Clients.All.SendAsync("SendUpdate", log);
        }
    }
}