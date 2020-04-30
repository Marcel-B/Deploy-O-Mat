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
    public class Fooar
    {
        public string Id { get; set; }
        public string Service { get; set; }
        public string Image { get; set; }
        public string Replicas { get; set; }
    }
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
            var l = new List<Fooar>();
            foreach (var log in logs)
            {
                l.Add(new Fooar
                {
                    Id = log.Id.ToString(),Image = log.Image,
                    Replicas = $"{log.ReplicasOnline}/{log.Replicas}",
                    Service = log.ServiceId
                });
            }
            var lst =  JsonSerializer.Serialize(l);
            await Clients.All.SendAsync("SendUpdate", l);
        }
    }
}