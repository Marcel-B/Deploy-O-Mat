using System;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models
{
    public  class VolumeOption
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public virtual Mount Mount { get; set; }
        public Guid MountId { get; set; }
    }
}