using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace com.b_velop.Deploy_O_Mat.Web.Application.SignalR
{
    public class DockerServiceUpdateHub : Hub
    {
        private readonly IMediator _mediator;

        public DockerServiceUpdateHub(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendUpdate(
            string values)
        {
            await Clients.All.SendAsync("SendUpdate", values);
        }
    }
}
