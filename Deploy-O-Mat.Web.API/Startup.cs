using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using com.b_velop.Deploy_O_Mat.Queue.Infra.IoC;
using com.b_velop.Deploy_O_Mat.Web.API.Middlewares;
using com.b_velop.Deploy_O_Mat.Web.Application.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.DockerImage;
using com.b_velop.Deploy_O_Mat.Web.Application.Helpers;
using com.b_velop.Deploy_O_Mat.Web.Application.Services;
using com.b_velop.Deploy_O_Mat.Web.Application.SignalR;
using com.b_velop.Deploy_O_Mat.Web.Application.User;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using com.b_velop.Utilities.Docker;
using FluentValidation.AspNetCore;
using MediatR;
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
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("WWW-Authenticate")
                        .AllowCredentials()
                        .WithOrigins("http://localhost:3000", "http://localhost:3001");
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(List.Handler));
            
            var secretProvider = new SecretProvider();
            var password = secretProvider.GetSecret("postgres_db_password") ?? "";
            var username = secretProvider.GetSecret("username") ?? "";
            var host = secretProvider.GetSecret("host") ?? "";

            services.AddSignalR();

            var connection = $"Host={host};Port=5432;Username={username};Password={password};Database=DeployOMat;";

            services.AddScoped<IDockerStackLogService, DockerStackLogService>();
            services.AddDbContext<WebContext>(options =>
            {
                if (Env.IsDevelopment())
                    connection = Configuration.GetConnectionString("postgres");

                options.UseNpgsql(connection);
                options.UseLazyLoadingProxies();
            });

            var builder = services.AddIdentityCore<AppUser>();

            var identityBuilder = new IdentityBuilder(builder.UserType, typeof(AppRole), builder.Services);

            identityBuilder.AddEntityFrameworkStores<WebContext>();
            identityBuilder.AddRoleValidator<RoleValidator<AppRole>>();
            identityBuilder.AddRoleManager<RoleManager<AppRole>>();

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
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if(!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/info")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
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
            // app.UseCspReportOnly(opt => opt.BlockAllMixedContent()
            app.UseCsp(opt => opt.BlockAllMixedContent()
                .StyleSources(_ => _.Self().CustomSources("https://fonts.googleapis.com"))
                .FontSources(_ => _.Self().CustomSources("https://fonts.gstatic.com", "data:"))
                .FormActions(_ => _.Self())
                .FrameAncestors(_ => _.Self())
                .ImageSources(_ => _.Self().CustomSources("https://github.com"))
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
                endpoints.MapHub<DockerServiceUpdateHub>("/info");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}