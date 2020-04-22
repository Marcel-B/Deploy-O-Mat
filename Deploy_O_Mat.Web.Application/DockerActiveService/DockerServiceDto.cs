using System;
namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class DockerServiceDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastRestart { get; set; }
        public DateTime? Updated { get; set; }

        public string Name { get; set; }
        public string Repo { get; set; }
        public string Tag { get; set; }
        public DockerImageDto DockerImage { get; set; }
    }
}
