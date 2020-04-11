using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;

namespace com.b_velop.Deploy_O_Mat.Data.Repository
{
    public class RepositoryWrapper :  IRepositoryWrapper
    {
        public RepositoryWrapper(
            IDockerImageRepository dockerImageRepository,
            IDockerStackServiceRepository dockerStackServiceRepository,
            DataContext context)
        {
            DockerImages = dockerImageRepository;
            DockerStackServices = dockerStackServiceRepository;
            Context = context;
        }

        public IDockerImageRepository DockerImages { get; }

        public IDockerStackServiceRepository DockerStackServices { get; }
        public DataContext Context { get; }

        public async Task<bool> SaveChangesAsync()
        {
            var a =  await Context.SaveChangesAsync();
            return a > 0;
        }
    }
}
