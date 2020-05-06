using System;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public class VirtualIp
    {
        public Guid Id { get; set; }
        public string NetworkId { get; set; }
        public string Address { get; set; }
        public virtual DockerServiceDetail DockerServiceDetail { get; set; }
        public Guid DockerServiceDetailId { get; set; }
    }
}