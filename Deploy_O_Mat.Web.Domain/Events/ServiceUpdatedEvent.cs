using System;
using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
{
    public class ServiceUpdatedEvent : Event
    {
        public string ServiceName { get; set; }
        public string RepoName { get; set; }
        public string Tag { get; set; }
        public Guid BuildId { get; set; }

        public ServiceUpdatedEvent(
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
