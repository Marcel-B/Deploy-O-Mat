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
                    //var userManager = services.GetRequiredService<UserManager<User>>();
                    //var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    context.Database.Migrate();
                    context.SeedData();
                    //Seed.SeedUsers(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.Log(NLog.LogLevel.Fatal, ex, "An error occurred during migration");
                }
            }
            host.Run();
            //var psi = new ProcessStartInfo
            //{
            //    FileName = "docker",
            //    //Arguments = "-c 3 8.8.8.8",
            //    Arguments = "rm ad9ad06ae538",
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true
            //};

            //var process = new Process
            //{
            //    StartInfo = psi
            //};

            //process.Start();

            //while (!process.StandardOutput.EndOfStream)
            //{
            //    var line = await process.StandardOutput.ReadLineAsync();
            //    Console.WriteLine(line);
            //}
            //while (!process.StandardError.EndOfStream)
            //{
            //    var line = await process.StandardError.ReadLineAsync();
            //    Console.WriteLine(line);
            //}

            //process.WaitForExit();
            //Console.WriteLine(process.ExitCode);
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