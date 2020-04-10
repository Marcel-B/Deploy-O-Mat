using com.b_velop.Deploy_O_Mat.Application.Interfaces;
using com.b_velop.Deploy_O_Mat.Application.Services;
using com.b_velop.Deploy_O_Mat.Domain.CommandHandlers;
using com.b_velop.Deploy_O_Mat.Domain.Commands;
using com.b_velop.Deploy_O_Mat.Data.Context;

using Deploy_O_Mat.Service.Application.Interfaces;
using Deploy_O_Mat.Service.Application.Services;
using Deploy_O_Mat.Service.Data.Context;
using Deploy_O_Mat.Service.Data.Repository;
using Deploy_O_Mat.Service.Domain.EventHandlers;
using Deploy_O_Mat.Service.Domain.Events;
using Deploy_O_Mat.Service.Domain.Interfaces;

using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using com.b_velop.Utilities.Docker;
using com.b_velop.Deploy_O_Mat.Domain.Interfaces;
using com.b_velop.Deploy_O_Mat.Data.Repository;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<SecretProvider>();

            // Domain Bus
            services.AddTransient<IEventBus, RabbitMQBus>();

            //Subscriptions
            services.AddTransient<ServiceUpdateEventHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<ServiceUpdateEvent>, ServiceUpdateEventHandler>();

            //Domain Commands
            services.AddTransient<IRequestHandler<CreateServiceUpdateCommand, bool>, ServiceUpdateCommandHandler>();

            //Application Services
            services.AddTransient<IDockerServiceService, DockerServiceService>();
            services.AddTransient<IDockerImageService, DockerImageService>();

            //Data
            services.AddTransient<IDockerServiceRepository, DockerServiceRepository>();
            services.AddTransient<IDockerImageRepository, DockerImageRepository>();
            services.AddTransient<DockerServiceDbContext>();
            services.AddTransient<DataContext>();
        }
    }
}
