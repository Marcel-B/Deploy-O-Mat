using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Data.Context;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Data.Repository
{
    public class DockerImageRepository : IDockerImageRepository
    {
        private readonly DataContext _dataContext;

        public DockerImageRepository(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DockerImage CreateDockerImage(
            DockerImage dockerImage)
            => _dataContext.DockerImages.Add(dockerImage).Entity;

        public async Task<DockerImage> DeleteDockerImage(
            Guid id)
            => _dataContext.Remove(await _dataContext.DockerImages.FindAsync(id)).Entity;

        public async Task<DockerImage> GetDockerImage(
            Guid id)
         => await _dataContext.DockerImages.FindAsync(id);

        public async Task<List<DockerImage>> GetDockerImages()
         => await _dataContext.DockerImages.ToListAsync();

        public async Task<bool> SaveChanges()
            => await _dataContext.SaveChangesAsync() > 0;

        public async Task<DockerImage> UpdateDockerImage(
            Guid id,
            DockerImage dockerImage)
        {
            var tmpDockerImage = await _dataContext.DockerImages.FindAsync(dockerImage.Id);

            if (tmpDockerImage == null)
                throw new KeyNotFoundException($"Entity with key '{id}' not found");
            else
            {
                tmpDockerImage.BuildId = Guid.NewGuid();
                tmpDockerImage.Pusher = dockerImage.Pusher;
                tmpDockerImage.Tag = dockerImage.Tag;
                tmpDockerImage.Updated = dockerImage.Updated;
                tmpDockerImage.Dockerfile = dockerImage.Dockerfile;
            }
            return tmpDockerImage;
        }
    }
}
