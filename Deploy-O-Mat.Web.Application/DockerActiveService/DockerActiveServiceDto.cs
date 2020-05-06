using System;
using System.Collections;
using System.Collections.Generic;

namespace com.b_velop.Deploy_O_Mat.Web.Application.DockerActiveService
{
    public class DockerActiveServiceDto
    {
        public Guid Id { get; set; }

        public string ServiceName { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastRestart { get; set; }
        public DateTime? LastManualRestart { get; set; }

        public DockerServiceDto DockerService { get; set; }
        public DockerStackDto DockerStack { get; set; }

    }
}
