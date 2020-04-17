using System;
using System.Collections.Generic;
using System.Linq;
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

        public  Task CreateOrUpdateDockerStackLog(
            IEnumerable<DockerStackLog> stackLogs)
        {
            foreach (var stackLog in stackLogs)
            {
                if (stackLog.Image == null) continue;
                var current = _context.DockerStackLogs.FirstOrDefault(x => x.Image == stackLog.Image);
                var repoNameIdx = stackLog.Image.LastIndexOf(':');
                var repo = stackLog.Image.Substring(0,repoNameIdx);
                var dockerImage =  _context.DockerImages.FirstOrDefault(x => x.RepoName == repo);

                if (dockerImage != null)
                    stackLog.DockerImageId = dockerImage.Id;

                if (current == null)// no log entry
                {
                    stackLog.Id = Guid.NewGuid();
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
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
