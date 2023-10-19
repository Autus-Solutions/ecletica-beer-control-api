using EcleticaBeerControl.Application.Behaviors;
using EcleticaBeerControl.Application.Processors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EcleticaBeerControl.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR((configuration) =>
            {
                configuration.Lifetime = ServiceLifetime.Scoped;
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>), ServiceLifetime.Scoped);
                configuration.AddOpenBehavior(typeof(FailFastValidationBehavior<,>), ServiceLifetime.Scoped);
                configuration.AddOpenRequestPreProcessor(typeof(BaseDomainMetadataPreProcessor<>), ServiceLifetime.Scoped);
                configuration.AddOpenRequestPreProcessor(typeof(ClientBasedDomainMetadataPreProcessor<>), ServiceLifetime.Scoped);
            });

            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
