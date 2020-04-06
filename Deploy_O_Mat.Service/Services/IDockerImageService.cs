using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain;

namespace Deploy_O_Mat.Service.Services
{
    public interface IDockerImageService
    {
        Task<IEnumerable<DockerImage>> GetDockerImages();
    }
}
