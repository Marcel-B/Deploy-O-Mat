using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deploy_O_Mat.Service.Domain.Models
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
        public string Script { get; set; }
        public string Network { get; set; }

        [NotMapped]
        public string Repo
        {
            get
            {
                var repo = RepoName;
                if (!string.IsNullOrWhiteSpace(Tag))
                    repo += $":{Tag}";
                return repo;
            }
        }

        [NotMapped]
        public string Net
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Network))
                    return $"--network {Network}";
                return "";
            }
        }

    }
}
