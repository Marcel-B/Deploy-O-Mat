﻿using com.b_velop.Deploy_O_Mat.Service.Application.Services;
using Deploy_O_Mat.Service.Domain.EventHandlers;
using Deploy_O_Mat.Service.Domain.Interfaces;
using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using com.b_velop.Utilities.Docker;
using com.b_velop.Deploy_O_Mat.Web.Application.Services;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Data.Repository;
using Deploy_O_Mat.Web.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Web.Infrastructure.Security;
using Deploy_O_Mat.Service.Domain.Commands;
using Deploy_O_Mat.Service.Domain.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Shared;
using com.b_velop.Deploy_O_Mat.Shared.Contracts;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Web.Application.Bus.EventHandlers;
using com.b_velop.Deploy_O_Mat.Web.Data.Contracts;
using DockerServiceService = com.b_velop.Deploy_O_Mat.Service.Application.Services.DockerServiceService;
using DockerStackService = com.b_velop.Deploy_O_Mat.Service.Application.Services.DockerStackService;

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
            services.AddTransient<UpdateDockerServiceEventHandler>();
            services.AddTransient<CreateDockerStackEventHandler>();
            services.AddTransient<DockerInfoEventHandler>();
            services.AddTransient<SendDockerInfoEventHandler>();
            services.AddTransient<RemoveDockerServiceEventHandler>();
            services.AddTransient<CreateDockerServiceEventHandler>();
            services.AddScoped<UpdateServicesEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerServiceUpdatedEvent>, UpdateDockerServiceEventHandler>();
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerStackCreatedEvent>, CreateDockerStackEventHandler>();
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerInfoEvent>, DockerInfoEventHandler>();
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerStackRemovedEvent>, RemoveDockerStackEventHandler>();
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerServiceRemovedEvent>, RemoveDockerServiceEventHandler>();
            services.AddTransient<IEventHandler<Deploy_O_Mat.Service.Domain.Events.DockerServiceCreatedEvent>, CreateDockerServiceEventHandler>();
            services.AddScoped<IEventHandler<com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events.UpdateServicesEvent>, UpdateServicesEventHandler>();

            //Domain Commands
            services.AddTransient<IRequestHandler<CreateUpdateDockerServiceCommand, bool>, UpdateDockerServiceCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCreateStackCommand, bool>, CreateStackCommandHandler>();
            services.AddTransient<IRequestHandler<CreateSendDockerInfoCommand, bool>, SendDockerInfoCommandHandler>();
            services.AddTransient<IRequestHandler<DockerInfoCommand, bool>, DockerInfoCommandHandler>();
            services.AddTransient<IRequestHandler<CreateCreateDockerServiceCommand, bool>, CreateDockerServiceCommandHandler>();
            services.AddTransient<IRequestHandler<CreateRemoveDockerServiceCommand, bool>, RemoveDockerServiceCommandHandler>();

            // Utils
            services.AddScoped<IProcessor, Processor>();

            //Application Services
            services.AddTransient<Deploy_O_Mat.Service.Domain.Interfaces.IDockerServiceService, DockerServiceService>();

            services.AddScoped<com.b_velop.Deploy_O_Mat.Web.Application.Interfaces.IDockerServiceService, com.b_velop.Deploy_O_Mat.Web.Application.Services.DockerServiceService>();

            services.AddTransient<IDockerImageService, DockerImageService>();
            services.AddTransient<IDockerInfoService, DockerInfoService>();

            services.AddTransient<com.b_velop.Deploy_O_Mat.Web.Application.Interfaces.IDockerStackService, com.b_velop.Deploy_O_Mat.Web.Application.Services.DockerStackService>();

            services.AddTransient<Deploy_O_Mat.Service.Domain.Interfaces.IDockerStackService, DockerStackService>();

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            //Data
            services.AddTransient<IDeployOMatWebRepository, DeployOMatWebRepository>();
        }
    }
}
