using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Deploy_O_Mat.Web.Domain.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.SignalR
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