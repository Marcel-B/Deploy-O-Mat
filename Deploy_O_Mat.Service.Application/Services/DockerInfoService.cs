using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Application.Services
{
    public class DockerInfoService : IDockerInfoService
    {
        private readonly ILogger<DockerInfoService> _logger;
        private readonly IEventBus _bus;

        public DockerInfoService(
            ILogger<DockerInfoService> logger,
            IEventBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task<string> GetServices()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "docker",
                //Arguments = $"ps -a --no-trunc",
                Arguments = $"service ls",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process
            {
                StartInfo = psi
            };
            process.Start();
            var sb = new StringBuilder();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = await process.StandardOutput.ReadLineAsync();
                sb.AppendLine(line);
            }
            while (!process.StandardError.EndOfStream)
            {
                var error = await process.StandardError.ReadLineAsync();
                sb.AppendLine(error);
            }

            process.WaitForExit();
            sb.AppendLine($"Exit Code {process.ExitCode}");
            if (process.ExitCode != 0)
            {
                _logger.LogWarning($"Error while get service info");
            }
            return sb.ToString();
        }
    }
}
