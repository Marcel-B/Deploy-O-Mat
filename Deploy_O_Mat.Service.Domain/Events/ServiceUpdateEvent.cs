using System;
using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class ServiceUpdateEvent : Event
    {
        public string ServiceName { get; set; }
        public string RepoName { get; set; }
        public string Tag { get; set; }
        public Guid BuildId { get; set; }

        public ServiceUpdateEvent(
            string serviceName,
            string repoName,
            string tag,
            Guid buildId)
        {
            ServiceName = serviceName;
            RepoName = repoName;
            Tag = tag;
            BuildId = buildId;
        }
    }
}

