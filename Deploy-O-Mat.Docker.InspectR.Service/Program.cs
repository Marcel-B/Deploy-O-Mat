using System;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Commands.Services;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Events.ServiceDetail;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Services.Hosted;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Persistence.Context;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Infra.IoC;
using com.b_velop.Utilities.Docker;
using MediatR;
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
            try
            {
                var host = CreateHostBuilder(args).Build();
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<InspectRContext>();
                context.Database.Migrate();
                var eventBus = services.GetRequiredService<IEventBus>();
                eventBus.Subscribe<Details.DockerServiceDetailEvent, Details.DockerServiceDetailEventHandler>();
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
                    services.AddScoped<Details.DockerServiceDetailEventHandler>();
                    services.AddScoped<IInspectRRepository, InspectRRepository>();
                    services.AddScoped<IDockerServiceService, DockerServiceService>();
                    services.AddScoped<Create.UpdateServicesCommandHandler>();
                    services
                        .AddScoped<IRequestHandler<Create.UpdateServices, bool>, Create.UpdateServicesCommandHandler>();
                    services
                        .AddScoped<IEventHandler<Details.DockerServiceDetailEvent>,
                            Details.DockerServiceDetailEventHandler>();

                    services.AddDbContext<InspectRContext>(options =>
                    {
                        options.UseSqlite("Data Source=InspectR.db");
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