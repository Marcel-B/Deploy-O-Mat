using System;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public class Label
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual DockerServiceDetail DockerServiceDetail { get; set; }
        public Guid DockerServiceDetailId { get; set; }
    }
}