using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Data;
using Deploy_O_Mat.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service
{
    public class UpdateService : IHostedService
    {
        private readonly ILogger<UpdateService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _appLifetime;
        private Timer _timer;

        public UpdateService(
            ILogger<UpdateService> logger,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _timer = new Timer(RunJob, null, TimeSpan.Zero,
           TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");

            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        private async void RunJob(
            object state)
        {
            var services = new List<DockerService>();
            using var scope = _serviceProvider.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<IDockerImageService>();

            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var dockerImages = await httpClient.GetDockerImages();

            foreach (var dockerImage in dockerImages)
            {
                var dockerService = await dataContext.DockerServices.FindAsync(dockerImage.Id);
                if(dockerService == null || !dockerImage.IsActive)
                    _logger.LogInformation($"Service '{dockerImage.RepoName}:{dockerImage.Tag}' not active");
                else
                if(dockerService.BuildId != dockerImage.BuildId)
                {
                    dockerService.BuildId = dockerImage.BuildId;
                    _logger.LogInformation($"Try to update Docker Service '{dockerService.Name}' to BuildId '{dockerService.BuildId}'");
                    services.Add(dockerService);
                }
            }

            foreach (var dockerService in services)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "docker",
                    //Arguments = "-c 3 8.8.8.8",
                    Arguments = $"service update --image {dockerService.RepoName}:{dockerService.Tag} {dockerService.Name}",
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
                    error= await process.StandardError.ReadLineAsync();
                }

                process.WaitForExit();
                if(process.ExitCode == 0)
                {
                    dockerService.BuildId = dockerImages.First(_ => _.Id == dockerService.Id).BuildId;
                    dataContext.SaveChanges();
                    _logger.LogInformation($"Update Docker Service '{dockerService.Name}' to BuildId '{dockerService.BuildId}' completed");
                }
                else
                {
                    _logger.LogWarning($"Error while updating '{dockerService.Name}' to BuildId '{dockerService.BuildId}': ({process.ExitCode}) - {error}");
                }
                Console.WriteLine(process.ExitCode);
            }
        }
    }
}