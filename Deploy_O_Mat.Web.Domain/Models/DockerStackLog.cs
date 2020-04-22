using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerStackLog
    {
        public Guid Id { get; set; }
        public DateTime? Updated { get; set; }
        public virtual DockerImage DockerImage { get; set; }
        public Guid? DockerImageId { get; set; }
        public string ServiceId { get; set; }
        public string Name { get; set; }
        public string Mode { get; set; }
        public int Replicas { get; set; }
        public int ReplicasOnline { get; set; }
        public string Image { get; set; }
        public string Ports { get; set; }
        public bool IsActive { get; set; }

        public Guid DockerActiveServiceId { get; set; }
        public virtual DockerActiveService DockerActiveService { get; set; }
    }
}
