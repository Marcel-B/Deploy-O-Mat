using System;

namespace com.b_velop.Deploy_O_Mat.Domain
{
    public class DockerImage
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Pusher { get; set; }
        public string Namespace { get; set; }
        public string Owner { get; set; }
        public string RepoName { get; set; }
        public string RepoUrl { get; set; }

        public Guid BuildId { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
