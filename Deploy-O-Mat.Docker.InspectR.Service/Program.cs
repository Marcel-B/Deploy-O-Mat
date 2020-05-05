using System;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services.Hosted;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context;
using com.b_velop.Utilities.Docker;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();
            IEventBus eventBus = null;
            var services = host.Services;
            try
            {
                var context = services.GetRequiredService<InspectRContext>();
                context.Database.Migrate();
                //context.SeedData();
                eventBus = services.GetRequiredService<IEventBus>();
                var secretProvider = services.GetRequiredService<SecretProvider>();
                var userName = secretProvider.GetSecret("rabbit_user") ?? "guest";
                var passWord = secretProvider.GetSecret("rabbit_pass") ?? "guest";
                var HostName = secretProvider.GetSecret("HOSTNAME") ?? "localhost";

                // eventBus.Subscribe<DockerServiceUpdatedEvent, UpdateDockerServiceEventHandler>();
                // eventBus.Subscribe<DockerStackCreatedEvent, CreateDockerStackEventHandler>();
                // eventBus.Subscribe<DockerInfoEvent, DockerInfoEventHandler>();
                // eventBus.Subscribe<DockerStackRemovedEvent, RemoveDockerStackEventHandler>();
                //
                // eventBus.Subscribe<DockerServiceCreatedEvent, CreateDockerServiceEventHandler>();
                // eventBus.Subscribe<DockerServiceRemovedEvent, RemoveDockerServiceEventHandler>();
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
                    services.AddAutoMapper(typeof(Program).Assembly);
                    services.AddHostedService<DockerServiceReport>();
                    services.AddScoped<IInspectRRepository, InspectRRepository>();
                    services.AddScoped<IDockerServiceService, DockerServiceService>();
                    services.AddScoped<UpdateServicesCommandHandler>();
                    services.AddScoped<IRequestHandler<CreateUpdateServices, bool>, UpdateServicesCommandHandler>();

                    services.AddDbContext<InspectRContext>(options =>
                    {
                        options.UseSqlite("Data Source=dockerService.db");
                        options.UseLazyLoadingProxies();
                    });
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