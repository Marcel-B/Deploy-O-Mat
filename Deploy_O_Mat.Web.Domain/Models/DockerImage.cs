using System;
using System.Collections.Generic;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerImage
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? StartTime { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Pusher { get; set; }
        public string Namespace { get; set; }
        public string Owner { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }
        public string Dockerfile { get; set; }
        public string FullDescription { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Stars { get; set; }
        public bool IsOfficial { get; set; }
        public bool IsPrivate { get; set; }

        public Guid BuildId { get; set; }
        public bool IsActive { get; set; } = false;

        public virtual ICollection<DockerStackService> DockerStackServices { get; set; }
        public virtual ICollection<Badge> Badges { get; set; }
    }
}
