using System;
using System.Collections.Generic;

namespace Deploy_O_Mat.Service.Data
{
    public static class Seed
    {
        public static void SeedData(
            this DataContext dataContext)
        {
            var seedData = new List<DockerService>
            {
                new DockerService
                {
                    Name= "air_angularair",
                    Id=Guid.Parse("956ea3d3-b054-40a0-b770-0ba7e22afd5e"),
                    Created = DateTime.Now,
                    BuildId = Guid.Empty,
                    RepoName = "millegalb/angularair",
                    Tag = "latest"
                },
                new DockerService
                {
                    Name= "feinstaub_feinstaubapi",
                    Id=Guid.Parse("ce197ecd-2225-430a-8611-740a6b7acebd"),
                    Created = DateTime.Now,
                    BuildId = Guid.Empty,
                    RepoName = "mbodenstein/feinstaubserver",
                    Tag = "latest"
                },
                new DockerService
                {
                    Name = "deploy-o-mat_deploy-o-mat",
                    Id = Guid.Parse("A71BB297-ACEA-4CB9-A1F4-AF0C45D2B512"),
                    Created = DateTime.Now,
                    BuildId = Guid.Empty,
                    RepoName = "millegalb/deploy-o-mat",
                    Tag = "latest"
                }
            };
            foreach (var dockerService in seedData)
            {
                var tmpData = dataContext.DockerServices.Find(dockerService.Id);
                if (tmpData == null)
                    dataContext.DockerServices.Add(dockerService);
            }
            dataContext.SaveChanges();
        }
    }
}
