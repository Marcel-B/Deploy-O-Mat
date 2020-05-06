using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DockerService> UpdateDockerService(DockerService dockerService)
        {
            var old = await _context.DockerServices.FirstOrDefaultAsync(d => d.Name == dockerService.Name);
            if (old == null)
            {
                return _context.DockerServices.Add(dockerService).Entity;
            }

            old.Image = dockerService.Image;
            old.Mode = dockerService.Mode;
            old.Port = dockerService.Port;
            old.Replicas = dockerService.Replicas;
            old.ReplicasActive = dockerService.ReplicasActive;
            old.ServiceId = dockerService.ServiceId;
            old.Tag = dockerService.Tag;
            old.Updated = DateTime.UtcNow;
            old.IsActive = dockerService.IsActive;
            return old;
        }

        public IQueryable<DockerService> DockerServices()
        {
            return  _context.DockerServices;
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}