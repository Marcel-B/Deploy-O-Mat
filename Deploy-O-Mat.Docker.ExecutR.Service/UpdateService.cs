using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Service
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
                TimeSpan.FromSeconds(10));
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

        private void RunJob(
            object state)
        {

        }
    }
}
