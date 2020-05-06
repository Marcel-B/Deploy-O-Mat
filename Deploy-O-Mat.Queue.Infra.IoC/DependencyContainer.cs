using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.EventHandlers;
using com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Events;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Infra.Bus;
using com.b_velop.Deploy_O_Mat.Shared;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Application.Services;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Data.Repository;
using com.b_velop.Deploy_O_Mat.Web.Infrastructure.Security;
using com.b_velop.Utilities.Docker;
using Deploy_O_Mat.Web.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DockerServiceService = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Services.DockerServiceService;
using DockerStackService = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Application.Services.DockerStackService;
using IDockerServiceService = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces.IDockerServiceService;
using IDockerStackService = com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Interfaces.IDockerStackService;

namespace com.b_velop.Deploy_O_Mat.Queue.Infra.IoC
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
            services.AddTransient<UpdateDockerServiceEventHandler>();
            services.AddTransient<CreateDockerStackEventHandler>();
            services.AddTransient<RemoveDockerServiceEventHandler>();
            services.AddTransient<CreateDockerServiceEventHandler>();
            services.AddScoped<UpdateServicesEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<DockerServiceUpdatedEvent>, UpdateDockerServiceEventHandler>();
            services.AddTransient<IEventHandler<DockerStackCreatedEvent>, CreateDockerStackEventHandler>();
            services.AddTransient<IEventHandler<DockerStackRemovedEvent>, RemoveDockerStackEventHandler>();
            services.AddTransient<IEventHandler<DockerServiceRemovedEvent>, RemoveDockerServiceEventHandler>();
            services.AddTransient<IEventHandler<DockerServiceCreatedEvent>, CreateDockerServiceEventHandler>();
            services.AddScoped<IEventHandler<com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events.UpdateServicesEvent>, UpdateServicesEventHandler>();

            //Domain Commands
            services.AddTransient<IRequestHandler<CreateUpdateDockerServiceCommand, bool>, UpdateDockerServiceCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCreateStackCommand, bool>, CreateStackCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCreateDockerServiceCommand, bool>, CreateDockerServiceCommandHandler>();
            services.AddTransient<IRequestHandler<CreateRemoveDockerServiceCommand, bool>, RemoveDockerServiceCommandHandler>();

            // Utils
            services.AddScoped<IProcessor, Processor>();

            //Application Services
            services.AddTransient<IDockerServiceService, DockerServiceService>();

            services.AddScoped<com.b_velop.Deploy_O_Mat.Web.Application.Interfaces.IDockerServiceService, com.b_velop.Deploy_O_Mat.Web.Application.Services.DockerServiceService>();

            services.AddTransient<IDockerImageService, DockerImageService>();

            services.AddTransient<com.b_velop.Deploy_O_Mat.Web.Application.Interfaces.IDockerStackService, com.b_velop.Deploy_O_Mat.Web.Application.Services.DockerStackService>();

            services.AddTransient<IDockerStackService, DockerStackService>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            //Data
            services.AddTransient<IDeployOMatWebRepository, DeployOMatWebRepository>();
        }
    }
}
