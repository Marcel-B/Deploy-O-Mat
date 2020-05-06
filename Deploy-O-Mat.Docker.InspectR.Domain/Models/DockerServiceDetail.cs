using System;
using System.Collections.Generic;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public class DockerServiceDetail
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public int Index { get; set; }
        public string Image { get; set; }
        
        public virtual ICollection<Label> Labels { get; set; }
        public virtual ICollection<Placement> Placements { get; set; }
        public virtual ICollection<EnvironmentVariable> EnvironmentVariables { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<VirtualIp> VirtualIps { get; set; }
        public virtual ICollection<Mount> Mounts { get; set; }
        public virtual ICollection<Argument> Arguments { get; set; }
    }
}