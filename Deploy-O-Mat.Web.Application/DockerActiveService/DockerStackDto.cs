using System;
using System.Collections.Generic;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class DockerStackDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public ICollection<DockerImageDto> DockerStackImages { get; set; }
    }
}
