namespace EcleticaBeerControl.Worker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR((configuration) =>
            {
                configuration.Lifetime = ServiceLifetime.Scoped;
                configuration.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }
    }
}
