using System.Collections.Generic;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Application.Interfaces
{
    public interface IDockerServiceService
    {
        IEnumerable<DockerService> GetDockerServices();
    }
}
