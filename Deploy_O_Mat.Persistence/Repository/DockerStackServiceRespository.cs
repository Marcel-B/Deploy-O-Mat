using System;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Data.Repository
{
    public class DockerStackServiceRespository : RepositoryBase<DockerStackService>, IDockerStackServiceRepository
    {
        public DockerStackServiceRespository(DataContext context, ILogger<RepositoryBase<DockerStackService>> logger) : base(context, logger)
        {
        }

        public override Task<bool> UpdateAsync(DockerStackService entity, DockerStackService old, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
