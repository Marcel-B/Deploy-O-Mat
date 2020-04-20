using System.Text;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Web.API.Middlewares;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerImages;
using com.b_velop.Deploy_O_Mat.Web.Application.Helpers;
using com.b_velop.Deploy_O_Mat.Web.Application.User;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using com.b_velop.Utilities.Docker;
using FluentValidation.AspNetCore;
using MediatR;
using MicroRabbit.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace com.b_velop.Deploy_O_Mat.Web.API
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
        public void ConfigureServices(
            IServiceCollection services)
        {
            services
                .AddControllers(opt =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    opt.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<Login>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy(Strings.CorsPolicy, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://localhost:3001");
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly, typeof(UpdateServiceCommandHandler).Assembly);
            services.AddAutoMapper(typeof(List.Handler));
            
            var secretProvider = new SecretProvider();
            var password = secretProvider.GetSecret("postgres_db_password") ?? "";
            var username = secretProvider.GetSecret("username") ?? "";
            var host = secretProvider.GetSecret("host") ?? "";


            //eventBus = services.GetRequiredService<IEventBus>();
            //var secretProvider = services.GetRequiredService<SecretProvider>();
            //var userName = secretProvider.GetSecret("rabbit_user") ?? "guest";
            //var passWord = secretProvider.GetSecret("rabbit_pass") ?? "guest";
            //var HostName = secretProvider.GetSecret("HOSTNAME") ?? "localhost";
            //Console.WriteLine(userName);
            //Console.WriteLine(passWord);
            //Console.WriteLine(HostName);
            //eventBus.Subscribe<ServiceUpdatedEvent, UpdateServiceEventHandler>();


            var connection = $"Host={host};Port=5432;Username={username};Password={password};Database=DeployOMat;";

            services.AddDbContext<WebContext>(options =>
            {
                if (Env.IsDevelopment())
                    connection = Configuration.GetConnectionString("postgres");

                options.UseNpgsql(connection);
                options.UseLazyLoadingProxies();
            });

            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<WebContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            var secret = secretProvider.GetSecret("identity_signing_key") ?? "only_a_test_foo_ab";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false, // Url is comming from
                        ValidateIssuer = false
                    };
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
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            // Security Headers
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opt => opt.NoReferrer());
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            app.UseXfo(opt => opt.Deny());
            //app.UseCspReportOnly(opt => opt.BlockAllMixedContent()
            app.UseCsp(opt => opt.BlockAllMixedContent()
                .StyleSources(_ => _.Self().CustomSources("https://fonts.googleapis.com"))
                .FontSources(_ => _.Self().CustomSources("https://fonts.gstatic.com", "data:"))
                .FormActions(_ => _.Self())
                .FrameAncestors(_ => _.Self())
                .ImageSources(_ => _.Self())
                .ScriptSources(_ => _.Self().CustomSources("sha256-A+1Ei6kIUokroRUVKDJgHC5aqwihcYIWZy/4BkF8hmo=")));
            // ---

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(Strings.CorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
