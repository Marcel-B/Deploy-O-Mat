using System;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerActiveService
    {
        public Guid Id { get; set; }

        // stack_name_ServiceName in case of stack
        // ServiceName in case of service
        public string ServiceName { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastRestart { get; set; }
        public DateTime? LastManualRestart { get; set; }

        public Guid DockerImageId { get; set; }
        public Guid? DockerServiceId { get; set; }
        public Guid? DockerStackId { get; set; }

        public virtual DockerImage DockerImage { get; set; }
        public virtual DockerService DockerService { get; set; }
        public virtual DockerStack DockerStack { get; set; }
    }
}
