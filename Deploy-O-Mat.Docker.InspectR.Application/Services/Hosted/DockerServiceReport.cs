using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services.Hosted
{
    public class DockerServiceReport : IHostedService
    {
        private readonly ILogger<DockerServiceReport> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _appLifetime;
        private Timer _timer;

        public DockerServiceReport(
            ILogger<DockerServiceReport> logger,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(
            CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _timer = new Timer(RunJob, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(
            CancellationToken cancellationToken)
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
            string services = "";
            using var scope = _serviceProvider.CreateScope();
            // var dockerInfoService = scope.ServiceProvider.GetRequiredService<IDockerInfoService>();
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            // await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
        }
    }
}