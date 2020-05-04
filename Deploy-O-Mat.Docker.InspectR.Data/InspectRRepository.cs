using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Data
{
    public class InspectRRepository : IInspectRRepository
    {
        private readonly InspectRContext _context;

        public InspectRRepository(
            InspectRContext context)
        {
            _context = context;
        }
        public DockerService AddDockerService(
            DockerService dockerService)
        {
            var entity = _context.DockerServices.Add(dockerService);
            return entity.Entity;
        }

        public DockerServiceDetail AddDockerServiceDetail(DockerServiceDetail dockerServiceDetail)
        {
            var entity = _context.DockerServiceDetails.Add(dockerServiceDetail);
            return entity.Entity;
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}