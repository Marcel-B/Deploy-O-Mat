using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Domain.Interfaces
{
    public interface IDockerImageRepository
    {
        Task<List<DockerImage>> GetDockerImages();
        Task<DockerImage> GetDockerImage(Guid id);
        DockerImage CreateDockerImage(DockerImage dockerImage);
        Task<DockerImage> UpdateDockerImage(Guid id, DockerImage dockerImage);
        Task<DockerImage> DeleteDockerImage(Guid id);
        Task<bool> SaveChanges();
    }
}
