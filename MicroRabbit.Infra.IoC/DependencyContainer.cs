using Deploy_O_Mat.Service.Application.Services;
using Deploy_O_Mat.Service.Data.Repository;
using Deploy_O_Mat.Service.Domain.EventHandlers;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;

using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using com.b_velop.Utilities.Docker;
using com.b_velop.Deploy_O_Mat.Web.Application.Services;
using com.b_velop.Deploy_O_Mat.Web.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Data.Repository;
using Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Infrastructure.Security;
using com.b_velop.Deploy_O_Mat.Web.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Web.Domain.CommandHandlers;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Domain.EventHandlers;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<SecretProvider>();

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, sp.GetService<SecretProvider>());
            });

            //Subscriptions
            services.AddTransient<UpdateServiceEventHandler>();
            services.AddTransient<CreateStackEventHandler>();
            services.AddTransient<DockerInfoEventHandler>();
            services.AddTransient<SendDockerInfoEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<ServiceUpdatedEvent>, UpdateServiceEventHandler>();
            services.AddTransient<IEventHandler<StackCreatedEvent>, CreateStackEventHandler>();
            services.AddTransient<IEventHandler<DockerInfoEvent>, DockerInfoEventHandler>();
            services.AddTransient<IEventHandler<com.b_velop.Deploy_O_Mat.Web.Domain.Events.SendDockerInfoEvent>, SendDockerInfoEventHandler>();

            //Domain Commands
            services.AddTransient<IRequestHandler<CreateUpdateServiceCommand, bool>, UpdateServiceCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCreateStackCommand, bool>, CreateStackCommandHandler>();
            services.AddTransient<IRequestHandler<CreateSendDockerInfoCommand, bool>, SendDockerInfoCommandHandler>();




            //Application Services
            services.AddTransient<IDockerServiceService, DockerServiceService>();
            services.AddTransient<IDockerImageService, DockerImageService>();
            services.AddTransient<IDockerInfoService, DockerInfoService>();
            services.AddTransient<com.b_velop.Deploy_O_Mat.Web.Application.Interfaces.IDockerStackService, com.b_velop.Deploy_O_Mat.Web.Application.Services.DockerStackService>();

            services.AddTransient<Deploy_O_Mat.Service.Domain.Interfaces.IDockerStackService, Deploy_O_Mat.Service.Application.Services.DockerStackService>();


            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            //Data
            services.AddTransient<IDockerServiceRepository, DockerServiceRepository>();
            services.AddTransient<IDockerImageRepository, DockerImageRepository>();
            services.AddTransient<IDockerStackServiceRepository, DockerStackServiceRespository>();
            //services.AddTransient<DockerServiceDbContext>();
            //services.AddTransient<WebContext>();
        }
    }
}
