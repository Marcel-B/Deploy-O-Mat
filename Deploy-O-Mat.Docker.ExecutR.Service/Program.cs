using System;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerStack;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Services;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Data.Repositories;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Persistence;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Infra.IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Create = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService.Create;
using Remove = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Bus.Events.DockerService.Remove;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                var host = CreateHostBuilder(args).Build();
                IEventBus eventBus = null;
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ExecutRContext>();
                context.Database.Migrate();

                //context.SeedData();
                eventBus = services.GetRequiredService<IEventBus>();
                eventBus.Subscribe<Update.DockerServiceUpdatedEvent, Update.UpdateDockerServiceEventHandler>();
                eventBus.Subscribe<Start.StartDockerStackEvent, Start.DockerStackEventEventHandler>();

                eventBus
                    .Subscribe<Application.Bus.Events.DockerStack.Create.DockerStackCreatedEvent,
                        Application.Bus.Events.DockerStack.Create.CreateDockerStackEventHandler>();
                eventBus
                    .Subscribe<Application.Bus.Events.DockerStack.Remove.DockerStackRemovedEvent,
                        Application.Bus.Events.DockerStack.Remove.RemoveDockerStackEventHandler>();

                eventBus.Subscribe<Create.DockerServiceCreatedEvent, Create.CreateDockerServiceEventHandler>();
                eventBus.Subscribe<Remove.DockerServiceRemovedEvent, Remove.RemoveDockerServiceEventHandler>();
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //hostContext.HostingEnvironment.EnvironmentName = "Development";
                    DependencyContainer.RegisterServices(services);
                    services.AddMediatR(typeof(Program));
                    // services.AddHostedService<UpdateService>();

                    services.AddScoped<IDockerStackService, DockerStackService>();
                    services.AddScoped<IDockerServiceService, DockerServiceService>();
                    services.AddScoped<IExecutRRepository, ExecutRRepository>();

                    // services
                    //     .AddScoped<IEventHandler<Update.DockerServiceUpdatedEvent>,
                    //         Update.UpdateDockerServiceEventHandler>();
                    // services
                    //     .AddScoped<IEventHandler<Application.Bus.Events.DockerStack.Create.DockerStackCreatedEvent>,
                    //         Application.Bus.Events.DockerStack.Create.CreateDockerStackEventHandler>();
                    // services
                    //     .AddScoped<IEventHandler<Application.Bus.Events.DockerStack.Remove.DockerStackRemovedEvent>,
                    //         Application.Bus.Events.DockerStack.Remove.RemoveDockerStackEventHandler>();
                    // services
                    //     .AddScoped<IEventHandler<Remove.DockerServiceRemovedEvent>,
                    //         Remove.RemoveDockerServiceEventHandler>();
                    // services
                    //     .AddScoped<IEventHandler<Create.DockerServiceCreatedEvent>,
                    //         Create.CreateDockerServiceEventHandler>();
                    //
                    // services.AddScoped<Update.UpdateDockerServiceEventHandler>();
                    // services.AddScoped<Application.Bus.Events.DockerStack.Create.CreateDockerStackEventHandler>();
                    // services.AddScoped<Remove.RemoveDockerServiceEventHandler>();
                    // services.AddScoped<Create.CreateDockerServiceEventHandler>();

                    services.AddDbContext<ExecutRContext>(options => { options.UseSqlite("Data Source=ExecutR.db"); });
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