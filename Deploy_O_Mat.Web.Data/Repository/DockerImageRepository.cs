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
    public class DockerImageRepository : IDockerImageRepository
    {
        private ILogger<DockerImageRepository> _logger;

        protected WebContext Context { get; }

        public DockerImageRepository(
            WebContext dataContext,
            ILogger<DockerImageRepository> logger)
        {
            Context = dataContext;
            _logger = logger;
        }

        public DockerImage Create(DockerImage entity, CancellationToken cancellationToken = default, bool saveChanges = true)
        {
            Context.DockerImages.Add(entity);
            return entity;
        }

        public DockerImage Update(
            DockerImage newDockerImage,
            DockerImage oldDockerImage,
            CancellationToken cancellationToken = default)
        {
            if (oldDockerImage == null)
                throw new ArgumentException($"Entity with key '{oldDockerImage.Id}' not found", nameof(oldDockerImage));

            else
            {
                oldDockerImage.BuildId = Guid.NewGuid();
                oldDockerImage.Pusher = newDockerImage.Pusher;
                oldDockerImage.Tag = newDockerImage.Tag;
                oldDockerImage.Updated = newDockerImage.Updated;
                oldDockerImage.Dockerfile = newDockerImage.Dockerfile;
            }
            return oldDockerImage;
        }

        public async Task<IEnumerable<DockerImage>> GetAll(
            CancellationToken cancellationToken = default)
        {
            return await Context.DockerImages.ToListAsync();
        }


        public async Task<DockerImage> Get(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await Context.DockerImages.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var success = await Context.SaveChangesAsync() > 0;
            return success;
        }
    }
}
