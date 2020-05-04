using System;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public class Port
    {
        public Guid Id { get; set; }
        public string Protocol { get; set; }
        public int TargetPort { get; set; }
        public int PublishedPort { get; set; }
        public string PublishMode { get; set; }
        public virtual DockerServiceDetail DockerServiceDetail { get; set; }
        public Guid DockerServiceDetailId { get; set; }
    }
}