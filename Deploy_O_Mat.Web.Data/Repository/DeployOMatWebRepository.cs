using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task CreateOrUpdateDockerStackLog(
            IEnumerable<DockerStackLog> stackLogs)
        {
            foreach (var stackLog in stackLogs)
            {
                var current = await _context.DockerStackLogs.FirstOrDefaultAsync(x => x.Image == stackLog.Image);
                var repoNameIdx = stackLog.Image.LastIndexOf(':');
                var repo = stackLog.Image.Substring(repoNameIdx);
                var dockerImage = await _context.DockerImages.FirstOrDefaultAsync(x => x.RepoName == repo);

                if (current == null)// no log entry
                {
                    current = stackLog;
                    _context.DockerStackLogs.Add(current);
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
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
