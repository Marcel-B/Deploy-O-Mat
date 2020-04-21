using System;
using System.Collections.Generic;
using System.Linq;
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
                if (stackLog.Image == null)
                    continue;
                var current = _context.DockerStackLogs.FirstOrDefault(x => x.Image == stackLog.Image);
                var repoNameIdx = stackLog.Image.LastIndexOf(':');
                var repo = stackLog.Image.Substring(0, repoNameIdx);
                var dockerImage = _context.DockerImages.FirstOrDefault(x => x.RepoName == repo);

                if (dockerImage != null)
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
                var stackLog = stackLogs.FirstOrDefault(_ => _.Image == log.Image);
                if (stackLog == null)
                {
                    log.IsActive = false;
                    continue;
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<DockerService> GetDockerService(
            Guid id)
        {
            var dockerService = await _context.DockerServices.FindAsync(id);
            return dockerService;
        }

        public async Task<IEnumerable<DockerService>> GetDockerServices()
            => await _context.DockerServices.ToListAsync();

        public async Task<IEnumerable<DockerStackLog>> GetDockerStackLogs()
            => await _context.DockerStackLogs.ToListAsync();

        public async Task<IEnumerable<DockerStack>> GetDockerStacks()
            => await _context.DockerStacks.ToListAsync();
    }
}
