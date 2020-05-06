using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Contracts
{
    public interface IDockerStackLogService
    {
        Task UpdateStackLogs(IEnumerable<Domain.Models.DockerStackLog> dockerStackLogs);
    }
}