using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerStackImage
    {
        public Guid DockerStackId { get; set; }
        public Guid DockerImageId { get; set; }
        public virtual DockerStack DockerStack { get; set; }
        public virtual DockerImage DockerImage { get; set; }
    }
}
