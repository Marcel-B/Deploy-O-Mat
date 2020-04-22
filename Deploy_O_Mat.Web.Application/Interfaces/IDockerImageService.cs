using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IDockerImageService
    {
        Task<IEnumerable<DockerImage>> GetDockerImages();
        Task<DockerImage> GetDockerImage(Guid id);
        Task<DockerImage> CreateOrUpdateDockerImage(DockerImage dockerImage);
        Task<IServiceResponse> UpdateDockerService(string service, string image);
    }
}
