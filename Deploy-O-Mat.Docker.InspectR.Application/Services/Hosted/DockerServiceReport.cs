using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands.Services;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
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

            _timer = new Timer(RunJob, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
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
            using var scope = _serviceProvider.CreateScope();
            var dockerInfoService = scope.ServiceProvider.GetRequiredService<IDockerServiceService>();
            var repo = scope.ServiceProvider.GetRequiredService<IInspectRRepository>();
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
            
            var services = await dockerInfoService.GetDockerServices();
            var dockerServices = services as DockerService[] ?? services.ToArray();
            
            foreach (var service in dockerServices)
                _ = await repo.UpdateDockerService(service);

            await repo.SaveChanges();

            await eventBus.SendCommand(new Create.UpdateServices {DockerServices = dockerServices});
            // var ids = services.Select(s => s.ServiceId);
            // var details = await dockerInfoService.GetDockerServiceDetails(ids);
            // await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
        }
    }
}