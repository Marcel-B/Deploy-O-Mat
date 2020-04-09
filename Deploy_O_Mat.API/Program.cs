using System;
using com.b_velop.Deploy_O_Mat.Persistence;
using com.b_velop.Utilities.Docker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace com.b_velop.Deploy_O_Mat.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var host = CreateHostBuilder(args).Build();
            var secretProvider = new SecretProvider();

            var userName = secretProvider.GetSecret("RABBITMQ_DEFAULT_USER_FILE") ?? "guest";
            var passWord = secretProvider.GetSecret("RABBITMQ_DEFAULT_PASS_FILE") ?? "guest";
            var ho = secretProvider.GetSecret("HOSTNAME") ?? "rabbitmq";

            logger.Log(NLog.LogLevel.Fatal, userName);
            logger.Log(NLog.LogLevel.Fatal, passWord);
            logger.Log(NLog.LogLevel.Fatal, ho);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {

                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    Seed.SeedData(context);
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
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
