using System;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerService
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastRestart { get; set; }
        public DateTime? Updated { get; set; }

        public string Name { get; set; }
        public string Repo { get; set; }
        public string Tag { get; set; }
        public string Network { get; set; }
        public string Script { get; set; }

        public bool IsActive { get; set; }

        public DockerService()
        {
            Created = DateTime.UtcNow;
        }

        public virtual DockerImage DockerImage { get; set; }
        public Guid DockerImageId { get; set; }

        public string Image
        {
            get
                => $"{Repo}:{Tag}";
        }
    }
}
