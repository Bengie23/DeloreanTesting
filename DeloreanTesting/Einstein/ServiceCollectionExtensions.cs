using Microsoft.Extensions.DependencyInjection;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// work in progress
    /// </summary>
    static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a <typeparamref name="type"/> decorator on top of the previous registration of that type.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddDecorator(
            this IServiceCollection services,
            Type type,
            ServiceLifetime? lifetime = null)
        {
            // By convention, the last registration wins
            var previousRegistration = services.LastOrDefault(
                descriptor => descriptor.ServiceType == type);

            if (previousRegistration is null)
                throw new InvalidOperationException($"Tried to register a decorator for type {type.Name} when no such type was registered.");

            // Get a factory to produce the original implementation
            var decoratedServiceFactory = previousRegistration.ImplementationFactory;
            if (decoratedServiceFactory is null && previousRegistration.ImplementationInstance != null)
                decoratedServiceFactory = _ => previousRegistration.ImplementationInstance;
            if (decoratedServiceFactory is null && previousRegistration.ImplementationType != null)
                decoratedServiceFactory = serviceProvider => ActivatorUtilities.CreateInstance(
                    serviceProvider, previousRegistration.ImplementationType, Array.Empty<object>());

            if (decoratedServiceFactory is null) // Should be impossible
                throw new Exception($"Tried to register a decorator for type {type.Name}, but the registration being wrapped specified no implementation at all.");

            Func<IServiceProvider, object> trackingFactory = serviceProvider =>
            {
                serviceProvider.GetRequiredService<IDependencyTracker>().TrackDependency(previousRegistration.ImplementationType, previousRegistration.ServiceType);
                var dependency = decoratedServiceFactory(serviceProvider);
                return dependency;
            };
            var registration = new ServiceDescriptor(
                type, trackingFactory, lifetime ?? previousRegistration.Lifetime);

            services.Add(registration);

            return services;
        }

        public static IEnumerable<ServiceDescriptor?> FilterServicesBy(this IServiceCollection services, IReadOnlyCollection<string> dependencies)
        {
            return services.Where(s => 
                dependencies.Contains(s.ServiceType?.Name) 
                || dependencies.Contains(s.ImplementationInstance?.GetType().Name) 
                || dependencies.Contains(s.ImplementationType?.Name))
                .ToList();
            
        }
    }
}
