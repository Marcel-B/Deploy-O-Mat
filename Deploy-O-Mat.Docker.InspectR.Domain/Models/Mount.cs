using System;
using System.Collections.Generic;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public class Mount
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public virtual ICollection<VolumeOption> VolumeOptions { get; set; }
    }
}