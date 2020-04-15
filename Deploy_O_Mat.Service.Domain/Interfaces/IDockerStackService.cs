using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Models;

namespace Deploy_O_Mat.Service.Domain.Interfaces
{
    public interface IDockerStackService
    {
        Task<int> CreateStack(DockerStack stack);
        Task<int> RemoveStack(DockerStack stack);
    }
}
