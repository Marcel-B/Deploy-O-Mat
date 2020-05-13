using System;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using com.b_velop.Deploy_O_Mat.Web.Persistence;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using RabbitMQ.Client.Exceptions;

namespace com.b_velop.Deploy_O_Mat.Web.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<WebContext>();
            var eventBus = services.GetRequiredService<IEventBus>();

            try
            {
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                context.Database.Migrate();
                await Seed.SeedData(context, userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
            }
            try
            {
                eventBus.Subscribe<UpdateServicesEvent, UpdateServicesEventHandler>();
            }
            catch(BrokerUnreachableException ex)
            {
                logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during connection");
            }
            catch (Exception ex)
            {
                logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel(x => x.AddServerHeader = false)
                    .UseStartup<Startup>()
                    .UseUrls("http://*:5000");
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
