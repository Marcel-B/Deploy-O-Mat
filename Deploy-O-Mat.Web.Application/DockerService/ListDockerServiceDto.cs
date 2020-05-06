using System;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerService
{
    public class ListDockerServiceDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastRestart { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
    }
}