using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.ProcessModels;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using System.IO;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IProcessor _processor;
        private readonly IMapper _mapper;
        private readonly IInspectRRepository _repo;
        private readonly ILogger<DockerServiceService> _logger;

        public DockerServiceService(
            IProcessor processor,
            IMapper mapper,
            IInspectRRepository repo,
            ILogger<DockerServiceService> logger)
        {
            _processor = processor;
            _mapper = mapper;
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<DockerService>> GetDockerServices()
        {
            var consoleResult = string.Empty;
#if DEBUG
            var path = Path.Combine(AppContext.BaseDirectory, "services.txt");
            consoleResult = await System.IO.File.ReadAllTextAsync(path);
#else
            var processResult = await _processor.Process("docker", "service ls");
            if (!processResult.Success)
            {
                _logger.LogError(processResult.ErrorMessage);
                return null;
            }
            consoleResult = processResult.Result;
#endif

            var lines = consoleResult.Split(Environment.NewLine);
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
                    Port = ports,
                    Created = DateTime.Now,
                    IsActive = true
                });
            }

            return result;
        }

        public async Task<DockerServiceDetail> GetDockerServiceDetail(string serviceId)
        {
            var processResult = await _processor.Process("docker", $"inspect {serviceId}");
            if (!processResult.Success)
            {
                _logger.LogError(processResult.ErrorMessage);
                return null;
            }

            return null;
        }

        private async Task<IEnumerable<Guid>> UpdateServices(IEnumerable<DockerService> services)
        {
            var result = new List<Guid>();
            foreach (var service in services)
            {
                var tmp = await _repo.UpdateDockerService(service);
                result.Add(tmp.Id);
            }
            return result;
        }
        
        public async Task<bool> UpdateDockerServices()
        {
            var services = await GetDockerServices();
            var ids = await UpdateServices(services);
            var deactiveServices =  _repo.DockerServices().Where(d => !ids.Contains(d.Id));
            foreach (var deactiveService in deactiveServices)
            {
                deactiveService.IsActive = false;
            }
            await _repo.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<DockerServiceDetail>> GetDockerServiceDetails(
            IEnumerable<string> serviceIds)
        {
            var consoleResult = string.Empty;
#if DEBUG
            consoleResult = await System.IO.File.ReadAllTextAsync("inspect.json");
#else
            var processResult = await _processor.Process("docker", $"inspect {string.Join(' ', serviceIds)}");
            if (!processResult.Success)
            {
                _logger.LogError(processResult.ErrorMessage);
                return null;
            }
            consoleResult = processResult.Result;
#endif

            var results = JsonSerializer.Deserialize<IEnumerable<InspectService>>(consoleResult);
            // var ret = _mapper.Map<IEnumerable<DockerServiceDetail>>(result);
            var ret = new List<DockerServiceDetail>();
            foreach (var result in results)
            {
                var detail = new DockerServiceDetail
                {
                    Name = result.Spec.Name,
                    Created = result.CreatedAt,
                    Updated = result.UpdatedAt,
                    ServiceId =  result.Id,
                    Index = result.Version.Index,
                    Image = result.Spec.TaskTemplate.ContainerSpec.Image,
                    Ports = result.Endpoint.Ports?.Select(p => new Domain.Models.Port
                    {
                        Protocol = p.Protocol,
                        PublishedPort = p.PublishedPort,
                        TargetPort = p.TargetPort,
                        PublishMode = p.PublishMode,
                    })?.ToList(),
                    Labels = result.Spec.Labels?.Select(l => new Domain.Models.Label
                    {
                        Name = $"{l.Key}={l.Value}"
                    })?.ToList(),
                    // Placements =  result.Spec.TaskTemplate.Placement(p => new Domain.Models.Placement
                    // {
                    //     Name = p.
                    // })
                };
                ret.Add(detail);
            }
            return ret;
        }
    }
}