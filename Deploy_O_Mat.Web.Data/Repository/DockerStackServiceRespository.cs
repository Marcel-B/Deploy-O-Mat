using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Repository
{
    public class DockerStackServiceRespository :  IDockerStackServiceRepository
    {
        protected WebContext Context { get;}

        private ILogger<DockerStackServiceRespository> _logger;

        public DockerStackServiceRespository( 
            WebContext context,
            ILogger<DockerStackServiceRespository> logger)
        {
            Context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<DockerStackService>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Context.DockerStackServices.ToListAsync();
        }

        public async Task<DockerStackService> Get(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await Context.DockerStackServices.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
