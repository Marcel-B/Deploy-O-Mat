using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace com.b_velop.Deploy_O_Mat.Web.API.SignalR
{
    public class ServiceUpdateHub : Hub
    {
        private readonly IMediator _mediator;

        public ServiceUpdateHub(
            IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task SendUpdate()
        {
            
        }
    }
}