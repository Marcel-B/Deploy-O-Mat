using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IProcessor _processor;
        private readonly ILogger<DockerServiceService> _logger;

        public DockerServiceService(
            IProcessor processor,
            ILogger<DockerServiceService> logger)
        {
            _processor = processor;
            _logger = logger;
        }

        public async Task<IEnumerable<DockerService>> GetDockerServices()
        {
            var processResult = await _processor.Process("docker", "service ls");
            if (!processResult.Success)
            {
                _logger.LogError(processResult.ErrorMessage);
                return null;
            }

            var lines = processResult.Result.Split(Environment.NewLine);
            var result = new List<DockerService>();
            var header = lines.FirstOrDefault();
            if (header == null) return null;
            var nameIdx = header.IndexOf("NAME", StringComparison.Ordinal);
            var modeIdx = header.IndexOf("MODE", StringComparison.Ordinal);
            var replicasIdx = header.IndexOf("REPLICAS", StringComparison.Ordinal);
            var imageIdx = header.IndexOf("IMAGE", StringComparison.Ordinal);
            var portsIdx = header.IndexOf("PORTS", StringComparison.Ordinal);

            foreach (var line in lines.Skip(1))
            {
                var tmp = line.Trim();
                if (tmp.Length <= 0) continue;

                var id = tmp[0..nameIdx].Trim();
                var name = tmp[nameIdx..modeIdx].Trim();
                var mode = tmp[modeIdx..replicasIdx].Trim();
                var replicas = tmp[replicasIdx..imageIdx].Trim();
                string image = null;
                string ports = null;
                if (tmp.Length >= portsIdx)
                {
                    image = tmp[imageIdx..portsIdx].Trim();
                    ports = tmp[portsIdx..].Trim();
                }
                else
                {
                    image = tmp[imageIdx..].Trim();
                }
                result.Add(new DockerService
                {
                    ServiceId = id,
                    Name = name,
                    Mode = mode,
                    Image = image.Substring(0, image.LastIndexOf(':')),
                    Tag = image.Substring(image.LastIndexOf(':') + 1),
                    Replicas = int.Parse(replicas.Split('/')[1]),
                    ReplicasActive = int.Parse(replicas.Split('/').First()),
                    Port =  ports,
                    Created = DateTime.Now,
                });
            }

            return result;
        }
    }
}