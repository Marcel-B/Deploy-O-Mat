using AutoMapper;
using com.b_velop.Deploy_O_Mat.API.Middlewares;
using com.b_velop.Deploy_O_Mat.Application.Helpers;
using com.b_velop.Deploy_O_Mat.Application.Images;
using com.b_velop.Deploy_O_Mat.Persistence;
using com.b_velop.Utilities.Docker;
using MediatR;
using MicroRabbit.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace com.b_velop.Deploy_O_Mat.API
{
    public class Startup
    {
        public Startup(
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            Env = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(Strings.CorsPolicy, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(List.Handler));
            var secretProvider = new SecretProvider();
            services.AddScoped<SecretProvider>();
            var password = secretProvider.GetSecret("postgres_db_password") ?? "";
            var username = secretProvider.GetSecret("username") ?? "";
            var host = secretProvider.GetSecret("host") ?? "";

            var connection = $"Host={host};Port=5432;Username={username};Password={password};Database=DeployOMat;";
            services.AddDbContext<DataContext>(options =>
            {
                if (Env.IsDevelopment())
                    connection = Configuration.GetConnectionString("postgres");
                options.UseNpgsql(connection);
            });
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to con/Users/marcel/source/repos/Deploy_O_Mat/deploy-o-mat/src/App.cssfigure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandlingMiddleware();
            app.UseRequestLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(Strings.CorsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
