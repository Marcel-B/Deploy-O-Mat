using System.Collections.Generic;
using Deploy_O_Mat.Service.Data.Context;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Data.Repository
{
    public class DockerServiceRepository : IDockerServiceRepository
    {
        private readonly DockerServiceDbContext _ctx;

        public DockerServiceRepository(
            DockerServiceDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<DockerService> GetDockerServices()
        {
            return _ctx.DockerServices;
        }
    }
}
