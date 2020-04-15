using System;
using System.Threading.Tasks;

namespace Deploy_O_Mat.Service.Domain.Interfaces
{
    public interface IDockerInfoService
    {
        Task<string> GetServices();
    }
}
