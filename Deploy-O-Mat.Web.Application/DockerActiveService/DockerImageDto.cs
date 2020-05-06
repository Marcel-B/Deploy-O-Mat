using System;
namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class DockerImageDto
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
        public string Status { get; set; }
    }
}
