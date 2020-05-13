using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts
{
    public interface IDockerStackService
    {
        Task<int> CreateStack(DockerStack stack);
        Task<int> StartStack(DockerStack stack);

        Task<int> RemoveStack(string stack);
    }
}