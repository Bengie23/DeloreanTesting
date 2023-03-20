using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Services.Common;
using System.Collections.ObjectModel;

namespace DeloreanTesting.Einstein
{
    /// <summary>
    /// Helper class to store the states of a collection of dependencies.
    /// </summary>
    internal static class TrackeableDependenciesDictionary
    {
        private static IServiceCollection registeredServices = new ServiceCollection();

        private static List<string> dependenciesUnderTest = new List<string>();

        /// <summary>
        /// Stores all dependencies coming from a service collection
        /// </summary>
        /// <param name="services"></param>
        public static void StoreRegisteredServices(IServiceCollection services)
        {
            registeredServices = services;
        }

        /// <summary>
        /// Evaluates if a given dependency name has been registered
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static bool HasBeenRegistered(string dependency)
        {
            return registeredServices.Any(s => s.ServiceType?.Name == dependency || s.ImplementationInstance?.GetType().Name == dependency || s.ImplementationType?.Name == dependency);
        }

        /// <summary>
        /// Store a list of dependencies under tests.
        /// </summary>
        /// <param name="dependencies"></param>
        public static void StoreDependenciesUnderTest(IReadOnlyCollection<string> dependencies)
        {
            if (dependencies.Any())
            {
                dependencies.ForEach(s =>
                {
                    dependenciesUnderTest.Add(s);
                });
            }
        }

        /// <summary>
        /// Returns the stored dependencies under test.
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyCollection<string> GetDependenciesUnderTest()
        {
            return new ReadOnlyCollection<string>(dependenciesUnderTest);
        }
    }
}
