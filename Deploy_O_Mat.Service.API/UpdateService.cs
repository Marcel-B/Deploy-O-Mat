using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Deploy_O_Mat.Service.Api
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

        public Task StartAsync(
            CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            _timer = new Timer(RunJob, null, TimeSpan.Zero,
#if DEBUG
            TimeSpan.FromSeconds(10));
#else
            TimeSpan.FromMinutes(5));
#endif
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
            var dockerInfoService = scope.ServiceProvider.GetRequiredService<IDockerInfoService>();
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

#if DEBUG
            services = File.ReadAllText("example.txt");
#else
            services = await dockerInfoService.GetServices();
#endif

            await eventBus.SendCommand(new CreateSendDockerInfoCommand(services));
        }
    }
}
