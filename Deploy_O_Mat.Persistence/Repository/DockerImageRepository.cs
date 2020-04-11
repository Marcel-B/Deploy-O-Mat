using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Data.Repository
{
    public class DockerImageRepository : RepositoryBase<DockerImage>, IDockerImageRepository
    {
        public DockerImageRepository(
            DataContext dataContext,
            ILogger<RepositoryBase<DockerImage>> logger) : base(dataContext, logger)
        {
        }

        public override async Task<bool> UpdateAsync(DockerImage entity, DockerImage old, CancellationToken cancellationToken = default)
        {
            if (old == null)
                throw new ArgumentException($"Entity with key '{entity.Id}' not found", nameof(old));

            else
            {
                old.BuildId = Guid.NewGuid();
                old.Pusher = entity.Pusher;
                old.Tag = entity.Tag;
                old.Updated = entity.Updated;
                old.Dockerfile = entity.Dockerfile;
            }
            var o = await Context.SaveChangesAsync();
            return o > 0;
        }
    }
}
