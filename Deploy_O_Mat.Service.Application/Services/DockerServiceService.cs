using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerServiceService : IDockerServiceService
    {
        private readonly IDockerServiceRepository _dockerServiceRepository;
        private readonly ILogger<DockerServiceService> _logger;
        private readonly IEventBus _bus;

        public DockerServiceService(
            IDockerServiceRepository dockerServiceRepository,
            ILogger<DockerServiceService> logger,
            IEventBus bus)
        {
            _dockerServiceRepository = dockerServiceRepository;
            _logger = logger;
            _bus = bus;
        }

        public IEnumerable<DockerService> GetDockerServices()
        {
            return _dockerServiceRepository.GetDockerServices();
        }

        public async Task<int> UpdateService(DockerService service)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "docker",
                //Arguments = "-c 3 8.8.8.8",
                Arguments = $"service update --image {service.RepoName}:{service.Tag} {service.Name}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process
            {
                StartInfo = psi
            };
            process.Start();
            var error = string.Empty;
            while (!process.StandardOutput.EndOfStream)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                _logger.LogInformation(line);
            }
            while (!process.StandardError.EndOfStream)
            {
                error = await process.StandardError.ReadLineAsync();
            }

            process.WaitForExit();
            if (process.ExitCode == 0)
            {
                //dockerService.BuildId = dockerImages.First(_ => _.Id == dockerService.Id).BuildId;
                //dataContext.SaveChanges();
                _logger.LogInformation($"Update Docker Service '{service.Name}' to BuildId '{service.BuildId}' completed");
                return process.ExitCode;
            }
            else
            {
                _logger.LogWarning($"Error while updating '{service.Name}' to BuildId '{service.BuildId}': ({process.ExitCode}) - {error}");
                return process.ExitCode;
            }
        }
    }
}
