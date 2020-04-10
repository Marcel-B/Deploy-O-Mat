using System;
namespace com.b_velop.Deploy_O_Mat.Application.Models
{
    public class DockerServiceUpdate
    {
        public string ServiceName { get; set; }
        public string RepoName { get; set; }
        public string Tag { get; set; }
        public Guid BuildId { get; set; }
    }
}
