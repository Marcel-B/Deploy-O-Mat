using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Application.Models;
using com.b_velop.Deploy_O_Mat.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Application.Interfaces
{
    public interface IDockerImageService
    {
        Task<IEnumerable<DockerImage>> GetDockerImages();
        Task<DockerImage> CreateOrUpdateDockerImage(DockerImage dockerImage);
        void UpdateDockerService(DockerServiceUpdate dockerServiceUpdate);
    }
}
