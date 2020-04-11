using System;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IDockerImageRepository DockerImages { get; }
        IDockerStackServiceRepository DockerStackServices { get; }
        Task<bool> SaveChangesAsync();
    }
}
