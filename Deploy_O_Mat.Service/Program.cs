using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Deploy_O_Mat.Service.Data;
using Deploy_O_Mat.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Deploy_O_Mat.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    context.SeedData();
                }
                catch (Exception ex)
                {
                    logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<UpdateService>();

                services.AddHttpClient<IDockerImageService, DockerImageService>();
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlite("Data Source=dockerService.db");
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