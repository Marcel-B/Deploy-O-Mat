using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.SignalR
{
    public interface IDockerServiceUpdate
    {
        Task SendUpdate(string log);
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
            List<DockerStackLog> logs)
        {
            var r = JsonSerializer.Serialize(logs);
            await Clients.All.SendAsync("SendUpdate", r);
        }
    }
}