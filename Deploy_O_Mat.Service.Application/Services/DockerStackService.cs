using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Interfaces;
using Deploy_O_Mat.Service.Domain.Models;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerStackService : IDockerStackService
    {
        private readonly ILogger<DockerStackService> _logger;
        private readonly IEventBus _bus;

        public DockerStackService(
            ILogger<DockerStackService> logger,
            IEventBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task<int> CreateStack(DockerStack stack)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = $"stack deploy -c {stack.File} {stack.Name}",
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
                _logger.LogInformation($"Docker Stack '{stack.Name}' with File '{stack.File}' created");
                return process.ExitCode;
            }
            else
            {
                _logger.LogWarning($"Error while creating Stack '{stack.Name}' with file '{stack.File}': ({process.ExitCode}) - {error}");
                return process.ExitCode;
            }
        }

        public async Task<int> RemoveStack(DockerStack stack)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = $"stack rm {stack.Name}",
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
                _logger.LogInformation($"Docker Stack '{stack.Name}' removed");
                return process.ExitCode;
            }
            else
            {
                _logger.LogWarning($"Error while removing Stack '{stack.Name}': ({process.ExitCode}) - {error}");
                return process.ExitCode;
            }
        }
    }
}
