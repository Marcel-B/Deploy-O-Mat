using System;
using com.b_velop.Utilities.Docker;
using Deploy_O_Mat.Service.Api.Services;
using Deploy_O_Mat.Service.Application.Interfaces;
using Deploy_O_Mat.Service.Application.Services;
using Deploy_O_Mat.Service.Data.Context;
using Deploy_O_Mat.Service.Data.Repository;
using Deploy_O_Mat.Service.Domain.EventHandlers;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;
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
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
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
                    Console.WriteLine(userName);
                    Console.WriteLine(passWord);
                    Console.WriteLine(HostName);
                    eventBus.Subscribe<ServiceUpdateEvent, ServiceUpdateEventHandler>();
                    host.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                DependencyContainer.RegisterServices(services);
                services.AddTransient<IDockerServiceService, DockerServiceService>();
                services.AddTransient<IDockerServiceRepository, DockerServiceRepository>();
                services.AddTransient<IEventHandler<ServiceUpdateEvent>, ServiceUpdateEventHandler>();
                services.AddHostedService<UpdateService>();
                services.AddTransient<SecretProvider>();
                services.AddHttpClient<IDockerImageService, DockerImageService>();
                services.AddMediatR(typeof(Program));
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