using System.Collections.Generic;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Domain.Interfaces
{
    public interface IDockerServiceRepository
    {
        IEnumerable<DockerService> GetDockerServices();
    }
}
