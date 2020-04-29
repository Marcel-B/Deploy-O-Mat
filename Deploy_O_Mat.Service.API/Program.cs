using System;
using com.b_velop.Utilities.Docker;
using Deploy_O_Mat.Service.Domain.EventHandlers;
using Deploy_O_Mat.Service.Domain.Events;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Deploy_O_Mat.Service.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();
            IEventBus eventBus = null;
            var services = host.Services;
            try
            {
                //var context = services.GetRequiredService<DockerServiceDbContext>();
                //context.Database.Migrate();
                //context.SeedData();
                eventBus = services.GetRequiredService<IEventBus>();
                var secretProvider = services.GetRequiredService<SecretProvider>();
                var userName = secretProvider.GetSecret("rabbit_user") ?? "guest";
                var passWord = secretProvider.GetSecret("rabbit_pass") ?? "guest";
                var HostName = secretProvider.GetSecret("HOSTNAME") ?? "localhost";

                eventBus.Subscribe<DockerServiceUpdatedEvent, UpdateDockerServiceEventHandler>();
                eventBus.Subscribe<DockerStackCreatedEvent, CreateDockerStackEventHandler>();
                eventBus.Subscribe<DockerInfoEvent, DockerInfoEventHandler>();
                eventBus.Subscribe<DockerStackRemovedEvent, RemoveDockerStackEventHandler>();

                eventBus.Subscribe<DockerServiceCreatedEvent, CreateDockerServiceEventHandler>();
                eventBus.Subscribe<DockerServiceRemovedEvent, RemoveDockerServiceEventHandler>();
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                DependencyContainer.RegisterServices(services);
                services.AddMediatR(typeof(Program));
                services.AddHostedService<UpdateService>();
                //services.AddDbContext<DockerServiceDbContext>(options =>
                //{
                //    options.UseSqlite("Data Source=dockerService.db");
                //});
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog();
    }
}