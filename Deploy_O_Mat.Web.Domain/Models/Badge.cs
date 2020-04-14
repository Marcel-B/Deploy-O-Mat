using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class Badge
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DockerImageId { get; set; }
        public virtual DockerImage DockerImage { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Badge()
        {
            Created = DateTime.UtcNow;
        }
    }
}
