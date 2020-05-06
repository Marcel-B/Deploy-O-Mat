using System;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Models
{
    public class DockerService
    {
        public Guid Id { get; set; }
        public string ServiceId { get; set; }
        public string Name { get; set; }
        public string Mode { get; set; }
        public int Replicas { get; set; }
        public int ReplicasActive { get; set; }
        public string Image { get; set; }
        public string Tag { get; set; }
        public string Port { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsActive { get; set; }
    }
}