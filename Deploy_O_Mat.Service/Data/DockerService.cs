using System;
namespace Deploy_O_Mat.Service.Data
{
    public class DockerService
    {
        public DockerService()
        {
            Created = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RepoName { get; set; }
        public string Tag { get; set; }
        public Guid BuildId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
