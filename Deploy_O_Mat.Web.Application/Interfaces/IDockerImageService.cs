using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IDockerImageService
    {
        Task<IEnumerable<Domain.Models.DockerImage>> GetDockerImages();
        Task<Domain.Models.DockerImage> GetDockerImage(Guid id);
        Task<Domain.Models.DockerImage> CreateOrUpdateDockerImage(Domain.Models.DockerImage dockerImage);
        Task<IServiceResponse> UpdateDockerService(string image, string service);
    }
}
