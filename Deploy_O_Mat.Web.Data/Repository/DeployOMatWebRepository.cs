﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Data.Repository
{
    public class DeployOMatWebRepository : IDeployOMatWebRepository
    {
        private WebContext _context;

        public DeployOMatWebRepository(
            WebContext context)
        {
            _context = context;
        }

        public Task CreateOrUpdateDockerStackLog(
            IEnumerable<DockerStackLog> stackLogs)
        {
            foreach (var stackLog in stackLogs)
            {
                if (stackLog.Name == null)
                    continue;

                var current = _context
                    .DockerStackLogs
                    .FirstOrDefault(x => x.Name == stackLog.Name);

                var repoNameIdx = stackLog.Image.LastIndexOf(':');
                var repo = stackLog.Image.Substring(0, repoNameIdx);

                var dockerImage = _context.DockerImages.FirstOrDefault(x => x.RepoName == repo);

                if (dockerImage == null)
                    continue;

                stackLog.DockerImageId = dockerImage.Id;


                if (current == null) // no log entry
                {
                    stackLog.Id = Guid.NewGuid();
                    stackLog.IsActive = true;
                    _context.DockerStackLogs.Add(stackLog);
                }
                else
                {
                    current.DockerImageId = dockerImage?.Id;
                    current.Image = stackLog.Image;
                    current.Mode = stackLog.Mode;
                    current.Updated = DateTime.UtcNow;
                    current.Ports = stackLog.Ports;
                    current.Replicas = stackLog.Replicas;
                    current.ReplicasOnline = stackLog.ReplicasOnline;
                    current.ServiceId = stackLog.ServiceId;
                    current.IsActive = true;
                }
            }
            var logs = _context.DockerStackLogs.ToList();
            foreach (var log in logs)
            {
                var stackLog = stackLogs.FirstOrDefault(_ => _.Name == log.Name);
                if (stackLog == null)
                {
                    log.IsActive = false;
                    continue;
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            return success;
        }

        public DockerImage UpdateDockerImage(
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

        public DockerImage CreateDockerImage(
            DockerImage entity,
            CancellationToken cancellationToken = default)
            => _context.DockerImages.Add(entity).Entity;

        public async Task<DockerImage> GetDockerImage(
            Guid id,
            CancellationToken cancellationToken = default)
            => await _context.DockerImages.FindAsync(id);

        public async Task<IEnumerable<DockerImage>> GetDockerImages(
           CancellationToken cancellationToken = default)
            => await _context.DockerImages.ToListAsync();

        public async Task<DockerService> GetDockerService(
            Guid id)
            => await _context.DockerServices.FindAsync(id);

        public async Task<IEnumerable<DockerService>> GetDockerServices()
            => await _context.DockerServices.ToListAsync();

        public async Task<DockerStack> GetDockerStack(Guid id)
            => await _context.DockerStacks.FindAsync(id);

        public async Task<IEnumerable<DockerStackLog>> GetDockerStackLogs()
            => await _context.DockerStackLogs.ToListAsync();

        public async Task<IEnumerable<DockerStack>> GetDockerStacks()
            => await _context.DockerStacks.ToListAsync();
    }
}
