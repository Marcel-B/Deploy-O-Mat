using System;
namespace com.b_velop.Deploy_O_Mat.Domain.Models
{
    public class DockerStackService
    {
        public DockerStackService()
        {
            Created = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public string MachineName { get; set; }

        public Guid DockerImageId { get; set; }
        public virtual DockerImage DockerImage { get; set; }
    }
}
