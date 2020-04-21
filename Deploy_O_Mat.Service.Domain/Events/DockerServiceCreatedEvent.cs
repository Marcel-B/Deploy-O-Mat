﻿using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class DockerServiceCreatedEvent : Event
    {
        public string Name { get; set; }
        public string Repo { get; set; }
        public string Tag { get; set; }
        public string Network { get; set; }
        public string Script { get; set; }

        public DockerServiceCreatedEvent(
            string name,
            string repo,
            string tag,
            string network,
            string script)
        {
            Name = name;
            Repo = repo;
            Tag = tag;
            Network = network;
            Script = script;
        }
    }
}